using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using System;
using System.Linq;

namespace GroupInvest.Microservices.Mensalidades.Domain.Servicos
{
    public class ServicoPeriodo : IServicoPeriodo
    {
        private readonly IRepositorioPeriodo repositorioPeriodo;
        private readonly IRepositorioMensalidade repositorioMensalidade;
        private readonly IRepositorioEmprestimo repositorioEmprestimo;
        private readonly IRepositorioAdesao repositorioAdesao;
        private readonly IRepositorioDistribuicaoCotas repositorioDistribuicaoCotas;
        private readonly IRepositorioPrevisaoPagamentoEmprestimo repositorioPrevisaoPagamentoEmprestimo;
        private readonly IServicoEmprestimo servicoEmprestimo;

        #region Construtor
        public ServicoPeriodo(
            IRepositorioPeriodo repositorioPeriodo,
            IRepositorioMensalidade repositorioMensalidade,
            IRepositorioEmprestimo repositorioEmprestimo,
            IRepositorioAdesao repositorioAdesao,
            IRepositorioDistribuicaoCotas repositorioDistribuicaoCotas,
            IRepositorioPrevisaoPagamentoEmprestimo repositorioPrevisaoPagamentoEmprestimo,
            IServicoEmprestimo servicoEmprestimo)
        {
            this.repositorioPeriodo = repositorioPeriodo;
            this.repositorioMensalidade = repositorioMensalidade;
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioAdesao = repositorioAdesao;
            this.repositorioDistribuicaoCotas = repositorioDistribuicaoCotas;
            this.repositorioPrevisaoPagamentoEmprestimo = repositorioPrevisaoPagamentoEmprestimo;
            this.servicoEmprestimo = servicoEmprestimo;
        }
        #endregion

        #region IServicoPeriodo
        public OperationResult IncluirPeriodo(Periodo periodo)
        {
            repositorioPeriodo.Incluir(periodo);
            if (repositorioPeriodo.Commit() > 0) return OperationResult.OK;
            else return new OperationResult(StatusCodeEnum.Error, "Commit não efetuou mudanças na base de dados");
        }

        public OperationResult AlterarPeriodo(Periodo periodo)
        {
            return OperationResult.OK;
        }

        public OperationResult ExcluirPeriodo(Periodo periodo)
        {
            return OperationResult.OK;
        }

        public OperationResult EncerrarPeriodo(Periodo periodo)
        {
            if (periodo == null || !periodo.Ativo)
                return new OperationResult(StatusCodeEnum.Error, "Período deve estar ativo para ser encerrado.");

            // cálculo do rendimento do período
            var valorPrevistoPeriodo = repositorioMensalidade.ObterPrevisaoRecebimentoMensalidades(periodo.DataTerminoPeriodo.GetValueOrDefault(), periodo);
            var valorDevidoMensalidades = repositorioMensalidade.ObterValorTotalDevidoMensalidades(periodo.DataTerminoPeriodo.GetValueOrDefault(), periodo);

            var result = servicoEmprestimo.ObterValorTotalReceberEmprestimos(periodo.DataTerminoPeriodo.GetValueOrDefault());
            if (result.StatusCode == StatusCodeEnum.Error)
                return result;

            var valorDevidoEmprestimos = (decimal)result.ObjectResult;
            var percentualRendimento = (valorDevidoMensalidades + valorDevidoEmprestimos - valorPrevistoPeriodo) / valorPrevistoPeriodo;

            // cálculo do valor a pagar por cota
            var numeroTotalCotas = repositorioAdesao.ObterTotalCotasPeriodo(periodo);
            var valorPagarCota = (valorDevidoMensalidades + valorDevidoEmprestimos) / numeroTotalCotas;

            // Distribuição de cotas
            var distribuicao = new DistribuicaoCotas
            {
                Periodo = periodo,
                NumeroTotalCotas = numeroTotalCotas,
                ValorPrevisto = valorPrevistoPeriodo,
                ValorArrecadado = valorDevidoMensalidades + valorDevidoEmprestimos,
                PercentualRendimento = Math.Round(percentualRendimento * 100, 2)
            };

            // Distribuição de cotas por participante
            var adesoes = repositorioAdesao.ListarAdesoesPeriodo(periodo);
            foreach (var adesao in adesoes)
            {
                var saldoDevedorMensalidades = repositorioMensalidade.ObterSaldoDevedorAdesao(adesao, periodo.DataTerminoPeriodo.GetValueOrDefault());
                var saldoDevedorEmprestimos = repositorioEmprestimo.ObterSaldoDevedorAdesao(adesao);
                var valorDistribuicao = valorPagarCota * adesao.NumeroCotas;
                var valorReceber = valorDistribuicao - saldoDevedorMensalidades - saldoDevedorEmprestimos;

                var distribuicaoParticipante = new DistribuicaoParticipante
                {
                    DistribuicaoCotas = distribuicao,
                    Participante = adesao.Participante,
                    SaldoDevedor = saldoDevedorMensalidades + saldoDevedorEmprestimos,
                    ValorDistribuicao = valorDistribuicao,
                    ValorTotalReceber = valorReceber
                };
                distribuicao.DistribuicaoParticipantes.Add(distribuicaoParticipante);
            }

            // gravação da distribuição
            repositorioDistribuicaoCotas.Incluir(distribuicao);
            if (repositorioDistribuicaoCotas.Commit() == 0)
                return new OperationResult(StatusCodeEnum.Error, "Erro ao gravar informações da distribuição de cotas.");

            // atualização do período
            periodo.Ativo = false;

            repositorioPeriodo.Alterar(periodo);
            if (repositorioPeriodo.Commit() > 0) return new OperationResult(distribuicao);
            else return new OperationResult(StatusCodeEnum.Error, "Erro ao gravar informações de período encerrado.");
        }

        public decimal ObterRendimentoParcial(DateTime dataReferencia)
        {
            var periodoAtivo = repositorioPeriodo.ObterPeriodoAtivo();

            if (periodoAtivo == null)
                return 0;

            var valorPrevisto = repositorioMensalidade.ObterPrevisaoRecebimentoMensalidades(dataReferencia, periodoAtivo);
            var valorDevidoMensalidades = repositorioMensalidade.ObterValorTotalDevidoMensalidades(dataReferencia, periodoAtivo);
            var valorDevidoEmprestimos = 0M;

            var previsoesPagamentoEmprestimo = repositorioPrevisaoPagamentoEmprestimo.ListarPrevisoesPagamentoEmprestimo(periodoAtivo, dataReferencia);
            if (previsoesPagamentoEmprestimo.Count > 0)
                valorDevidoEmprestimos = previsoesPagamentoEmprestimo.Sum(prev => prev.ValorDevido);

            var rendimento = ((valorDevidoMensalidades + valorDevidoEmprestimos - valorPrevisto) / valorPrevisto) * 100;

            return Math.Round(rendimento, 2);
        }

        public Periodo ObterPeriodoPorId(int id)
        {
            return repositorioPeriodo.ObterPorId(id);
        }

        public Periodo ObterPeriodoAtivo()
        {
            return repositorioPeriodo.ObterPeriodoAtivo();
        }
        #endregion
    }
}
