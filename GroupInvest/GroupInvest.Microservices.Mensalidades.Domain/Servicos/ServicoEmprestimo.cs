using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Helpers;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Mensalidades.Domain.Resources;
using System;
using System.Linq;
using System.Collections.Generic;

namespace GroupInvest.Microservices.Mensalidades.Domain.Servicos
{
    public class ServicoEmprestimo : IServicoEmprestimo
    {
        #region Injeção de dependência
        private readonly IRepositorioPeriodo repositorioPeriodo;
        private readonly IRepositorioAdesao repositorioAdesao;
        private readonly IRepositorioEmprestimo repositorioEmprestimo;
        private readonly IRepositorioPrevisaoPagamentoEmprestimo repositorioPrevisaoPagamento;
        #endregion

        #region Construtor
        public ServicoEmprestimo(
            IRepositorioPeriodo repositorioPeriodo,
            IRepositorioAdesao repositorioAdesao,
            IRepositorioEmprestimo repositorioEmprestimo,
            IRepositorioPrevisaoPagamentoEmprestimo repositorioPrevisaoPagamento)
        {
            this.repositorioPeriodo = repositorioPeriodo;
            this.repositorioAdesao = repositorioAdesao;
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioPrevisaoPagamento = repositorioPrevisaoPagamento;
        }
        #endregion

        #region IServicoEmprestimo
        public OperationResult ConcederEmprestimo(Participante participante, DateTime data, decimal valor)
        {
            var result = ObterAdesaoAtivaParticipante(participante);
            if (result.StatusCode == StatusCodeEnum.Error)
                return result;

            var adesaoAtiva = result.ObjectResult as Adesao;
            if (adesaoAtiva == null)
                return new OperationResult(StatusCodeEnum.Error, Mensagens.EmprestimoParticipanteSemAdesao);

            // criar objeto emprestimo
            var dataReferencia = new DateTime(data.Year, data.Month, data.Day); // retira a hora para a próxima data de vencimento
            var dataProximoVencimento = DateTimeHelper.ObterDataDiaUtil(dataReferencia.AddMonths(1));

            var emprestimo = new Emprestimo
            {
                Adesao = adesaoAtiva,
                Data = data,
                Valor = valor,
                DataProximoVencimento = dataProximoVencimento,
                Saldo = valor * (1 + (Parametrizacao.PercentualJurosEmprestimos / 100))
            };

            try
            {
                repositorioEmprestimo.Incluir(emprestimo);
                if (repositorioEmprestimo.Commit() > 0)
                {
                    // cria a previsão de pagamento
                    var previsaoPagamento = new PrevisaoPagamentoEmprestimo
                    {
                        Emprestimo = emprestimo,
                        DataVencimento = emprestimo.DataProximoVencimento.GetValueOrDefault(),
                        PercentualJuros = Parametrizacao.PercentualJurosEmprestimos,
                        ValorBase = emprestimo.Valor,
                        ValorDevido = emprestimo.Saldo,
                        Realizada = false,
                        Consolidada = false
                    };

                    repositorioPrevisaoPagamento.Incluir(previsaoPagamento);
                    if (repositorioPrevisaoPagamento.Commit() > 0) return new OperationResult(emprestimo);
                    else return new OperationResult(StatusCodeEnum.Error, Mensagens.CommitError);
                }
                else return new OperationResult(StatusCodeEnum.Error, Mensagens.CommitError);
            }
            catch (Exception ex)
            {
                return new OperationResult(StatusCodeEnum.Error, ex.Message);
            }
        }

        public OperationResult AtualizarPrevisaoPagamentoEmprestimo(Emprestimo emprestimo, DateTime dataReferencia)
        {
            if (emprestimo.Quitado)
                return OperationResult.OK;

            var previsoesPagamento = repositorioPrevisaoPagamento.ListarPrevisoesPagamentoEmprestimo(emprestimo, true)
                .Where(prev => prev.Consolidada == false);

            if (previsoesPagamento.Count() > 0)
            {
                // se a previsão de pagamento for data futura aborta a execução
                if (previsoesPagamento.First().DataVencimento >= dataReferencia)
                    return OperationResult.OK;

                #region Define quais os meses estão atrasados ou em aberto
                var datasAtrasadas = new Dictionary<DateTime, bool>(); // caso o mês deva ser consolidado, estará com true no valor

                var dataPrimeiraPrevisao = previsoesPagamento.First().DataVencimento;

                var dataAvaliacaoPrevisao = new DateTime(dataPrimeiraPrevisao.Year, dataPrimeiraPrevisao.Month, 1);
                var dataAvaliacaoReferencia = new DateTime(dataReferencia.Year, dataReferencia.Month, 1);

                while (dataAvaliacaoPrevisao < dataAvaliacaoReferencia)
                {
                    datasAtrasadas.Add(new DateTime(dataAvaliacaoPrevisao.Year, dataAvaliacaoPrevisao.Month, dataPrimeiraPrevisao.Day), true);
                    dataAvaliacaoPrevisao = dataAvaliacaoPrevisao.AddMonths(1);
                }
                datasAtrasadas.Add(new DateTime(dataAvaliacaoReferencia.Year, dataAvaliacaoReferencia.Month, dataPrimeiraPrevisao.Day), false);
                #endregion

                var valorBase = previsoesPagamento.First().ValorBase;
                var previsoesAtualizar = new List<PrevisaoPagamentoEmprestimo>();
                var novasPrevisoes = new List<PrevisaoPagamentoEmprestimo>();

                foreach (var dataAtrasada in datasAtrasadas)
                {
                    // caso exista a previsão de pagamento
                    var previsaoPagamento = previsoesPagamento.FirstOrDefault(prev => prev.DataVencimento.Equals(dataAtrasada.Key));
                    if (previsaoPagamento != null)
                    {
                        // consolida a previsão mudando o valor devido para somente valor dos juros
                        if (dataAtrasada.Value)
                        {
                            previsaoPagamento.PercentualJuros = Parametrizacao.PercentualJurosEmprestimosAtrasados;
                            previsaoPagamento.ValorDevido = previsaoPagamento.ValorBase * Parametrizacao.PercentualJurosEmprestimosAtrasados / 100;
                            previsaoPagamento.Consolidada = true;
                            previsoesAtualizar.Add(previsaoPagamento);
                        }
                        else
                        {
                            if (previsaoPagamento.DataVencimento < dataReferencia)
                            {
                                previsaoPagamento.PercentualJuros = Parametrizacao.PercentualJurosEmprestimosAtrasados;
                                previsaoPagamento.ValorDevido = previsaoPagamento.ValorBase * (1 + (Parametrizacao.PercentualJurosEmprestimosAtrasados / 100));
                                previsoesAtualizar.Add(previsaoPagamento);
                            }
                        }
                    }
                    else // cria uma nova previsão de pagamento para a data atrasada
                    {
                        var valorDevido = dataAtrasada.Value ?
                            valorBase * Parametrizacao.PercentualJurosEmprestimosAtrasados / 100 :
                            valorBase * (1 + (Parametrizacao.PercentualJurosEmprestimosAtrasados / 100));

                        var novaPrevisaoPagamento = new PrevisaoPagamentoEmprestimo
                        {
                            Emprestimo = emprestimo,
                            DataVencimento = dataAtrasada.Key,
                            ValorBase = valorBase,
                            PercentualJuros = Parametrizacao.PercentualJurosEmprestimosAtrasados,
                            ValorDevido = valorDevido,
                            Consolidada = dataAtrasada.Value
                        };
                        novasPrevisoes.Add(novaPrevisaoPagamento);
                    }
                }

                #region Gravação dos registros
                var saldoEmprestimo = 0M;
                foreach (var previsao in previsoesAtualizar)
                {
                    repositorioPrevisaoPagamento.Alterar(previsao);
                    saldoEmprestimo += previsao.ValorDevido;
                }

                foreach (var previsao in novasPrevisoes)
                {
                    repositorioPrevisaoPagamento.Incluir(previsao);
                    saldoEmprestimo += previsao.ValorDevido;
                }

                if (repositorioPrevisaoPagamento.Commit() == 0)
                    return new OperationResult(StatusCodeEnum.Error, string.Format("Erro ao atualizar previsões de pagamento para o empréstimo Id: {0}", emprestimo.Id));

                emprestimo.Saldo = saldoEmprestimo;
                repositorioEmprestimo.Alterar(emprestimo);
                if (repositorioEmprestimo.Commit().Equals(0))
                    return new OperationResult(StatusCodeEnum.Error, string.Format("Erro ao atualizar saldo do empréstimo Id: {0}", emprestimo.Id));
                #endregion
            }

            return OperationResult.OK;
        }

        public Emprestimo ObterEmprestimoParticipante(Participante participante, DateTime dataEmprestimo)
        {
            var result = ObterAdesaoAtivaParticipante(participante);
            if (result.StatusCode == StatusCodeEnum.Error)
                return null;

            var adesaoAtiva = result.ObjectResult as Adesao;
            if (adesaoAtiva == null)
                return null;

            return repositorioEmprestimo.ObterEmprestimo(adesaoAtiva, dataEmprestimo);
        }

        public Emprestimo ObterEmprestimoPorId(int emprestimoId)
        {
            return repositorioEmprestimo.ObterPorId(emprestimoId);
        }

        public PrevisaoPagamentoEmprestimo ObterPrevisaoPagamentoEmprestimoPorId(int previsaoPagamentoEmprestimoId)
        {
            return repositorioPrevisaoPagamento.ObterPorId(previsaoPagamentoEmprestimoId);
        }

        public IReadOnlyCollection<Emprestimo> ListarEmprestimosParticipante(Participante participante)
        {
            var result = ObterAdesaoAtivaParticipante(participante);
            if (result.StatusCode == StatusCodeEnum.Error)
                return new List<Emprestimo>();

            var adesaoAtiva = result.ObjectResult as Adesao;
            if (adesaoAtiva == null)
                return null;

            return repositorioEmprestimo.ListarEmprestimosAdesao(adesaoAtiva);
        }

        public IReadOnlyCollection<Emprestimo> ListarTodosEmprestimos()
        {
            var periodo = repositorioPeriodo.ObterPeriodoAtivo();
            return repositorioEmprestimo.ListarEmprestimosPeriodo(periodo);
        }

        public IReadOnlyCollection<Emprestimo> ListarEmprestimosEmAberto()
        {
            var periodo = repositorioPeriodo.ObterPeriodoAtivo();
            return repositorioEmprestimo.ListarEmprestimosEmAbertoPeriodo(periodo);
        }

        public IReadOnlyCollection<PrevisaoPagamentoEmprestimo> ListarPrevisoesPagamentoEmprestimo(Emprestimo emprestimo, bool apenasPendentes)
        {
            return repositorioPrevisaoPagamento.ListarPrevisoesPagamentoEmprestimo(emprestimo, apenasPendentes);
        }

        public OperationResult ObterValorTotalReceberEmprestimos(DateTime dataReferencia)
        {
            // atualiza todas as previsões de pagamento
            var emprestimos = ListarTodosEmprestimos();
            
            decimal valorPrevistoReceber = 0;
            foreach (var emprestimo in emprestimos)
            {
                var result = AtualizarPrevisaoPagamentoEmprestimo(emprestimo, dataReferencia);
                if (result.StatusCode == StatusCodeEnum.Error)
                    return result;

                var previsoesPagamento = ListarPrevisoesPagamentoEmprestimo(emprestimo, false);
                var previsaoEmprestimo = previsoesPagamento.Sum(prev => prev.ValorDevido);

                valorPrevistoReceber += previsaoEmprestimo;
            }

            return new OperationResult(valorPrevistoReceber);
        }

        public decimal ObterSaldoDevedorParticipante(Participante participante)
        {
            var result = ObterAdesaoAtivaParticipante(participante);
            if (result.StatusCode == StatusCodeEnum.Error)
                return 0;

            var adesaoAtiva = result.ObjectResult as Adesao;
            if (adesaoAtiva == null)
                return 0;

            return repositorioEmprestimo.ObterSaldoDevedorAdesao(adesaoAtiva);
        }
        #endregion

        #region Métodos Privados
        private OperationResult ObterAdesaoAtivaParticipante(Participante participante)
        {
            if (participante == null)
                return new OperationResult(StatusCodeEnum.Error, "Participante deve ser informado");

            var periodoAtivo = repositorioPeriodo.ObterPeriodoAtivo();
            if (periodoAtivo == null)
                return new OperationResult(StatusCodeEnum.Error, "Nenhum período ativo encontrado.");

            var adesao = repositorioAdesao.ObterAdesao(participante.Id, periodoAtivo.Id);
            return new OperationResult(adesao);
        }
        #endregion
    }
}
