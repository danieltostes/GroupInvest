using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Domain.Classes;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Mensalidades.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupInvest.Microservices.Mensalidades.Domain.Servicos
{
    public class ServicoPagamento : IServicoPagamento
    {
        #region Injeção de dependência
        private readonly IRepositorioPagamento repositorioPagamento;
        private readonly IRepositorioMensalidade repositorioMensalidade;
        private readonly IRepositorioEmprestimo repositorioEmprestimo;
        private readonly IRepositorioPagamentoParcialEmprestimo repositorioPagamentoParcial;
        private readonly IRepositorioPrevisaoPagamentoEmprestimo repositorioPrevisaoPagamento;
        private readonly IRepositorioPeriodo repositorioPeriodo;
        private readonly IRepositorioItemPagamento repositorioItemPagamento;
        #endregion

        #region Construtor
        public ServicoPagamento(IRepositorioPagamento repositorioPagamento,
            IRepositorioMensalidade repositorioMensalidade,
            IRepositorioEmprestimo repositorioEmprestimo,
            IRepositorioPagamentoParcialEmprestimo repositorioPagamentoParcial,
            IRepositorioPrevisaoPagamentoEmprestimo repositorioPrevisaoPagamento,
            IRepositorioPeriodo repositorioPeriodo,
            IRepositorioItemPagamento repositorioItemPagamento)
        {
            this.repositorioPagamento = repositorioPagamento;
            this.repositorioMensalidade = repositorioMensalidade;
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioPagamentoParcial = repositorioPagamentoParcial;
            this.repositorioPrevisaoPagamento = repositorioPrevisaoPagamento;
            this.repositorioPeriodo = repositorioPeriodo;
            this.repositorioItemPagamento = repositorioItemPagamento;
        }
        #endregion

        #region IServicoPagamento
        public OperationResult RealizarPagamento(Fatura fatura)
        {
            return RealizarPagamento(fatura, null);
        }

        public OperationResult RealizarPagamentoRetroativo(Fatura fatura)
        {
            return RealizarPagamento(fatura, new DateTime(fatura.DataPagamento.Year, fatura.DataPagamento.Month, 1));
        }

        public decimal ObterValorTotalRecebidoMensalidades(DateTime dataReferencia)
        {
            var periodoAtivo = repositorioPeriodo.ObterPeriodoAtivo();
            if (periodoAtivo == null)
                return 0;

            return repositorioItemPagamento.ObterValorTotalRecebidoMensalidades(dataReferencia, periodoAtivo);
        }

        public decimal ObterValorTotalRecebidoEmprestimos(DateTime dataReferencia)
        {
            var periodoAtivo = repositorioPeriodo.ObterPeriodoAtivo();
            if (periodoAtivo == null)
                return 0;

            return repositorioItemPagamento.ObterValorTotalRecebidoEmprestimos(dataReferencia, periodoAtivo);
        }
        #endregion

        #region Métodos Privados
        private OperationResult RealizarPagamento(Fatura fatura, DateTime? dataReferencia)
        {
            // validações a serem implementadas:
            //   Garantir que todos os itens possuem a mesma adesão
            //   Garantir que os valores pagos em cada item são no máximo o valor devido de cada um
            //   Garantir que todos os itens estão em aberto

            var pagamento = new Pagamento
            {
                DataPagamento = fatura.DataPagamento,
                ValorTotalPagamento = fatura.ValorTotal,
                ItensPagamento = fatura.Itens.ToList()
            };

            pagamento.ItensPagamento.ForEach(item => item.Pagamento = pagamento);

            try
            {
                // Grava o registro de pagamento
                repositorioPagamento.Incluir(pagamento);

                if (repositorioPagamento.Commit().Equals(0))
                    return new OperationResult(StatusCodeEnum.Error, "Erro ao gravar registro de pagamento.");

                #region Atualiza as mensalidades pagas
                if (fatura.Itens.Any(item => item.Mensalidade != null))
                {
                    foreach (var itemMensalidade in pagamento.ItensPagamento.Where(item => item.Mensalidade != null))
                    {
                        itemMensalidade.Mensalidade.ValorDevido = itemMensalidade.Valor;
                        itemMensalidade.Mensalidade.DataPagamento = fatura.DataPagamento;
                        itemMensalidade.Mensalidade.ValorPago = itemMensalidade.Valor;

                        repositorioMensalidade.Alterar(itemMensalidade.Mensalidade);
                    }
                    if (repositorioMensalidade.Commit().Equals(0))
                        return new OperationResult(StatusCodeEnum.Error, string.Format("Erro ao atualizar open de pagamento de mensalidades. Pagamento Id: {0}", pagamento.Id));
                }
                #endregion

                // Pagamento de previsões para os empréstimos
                if (fatura.Itens.Any(item => item.PrevisaoPagamentoEmprestimo != null))
                {
                    #region Realiza as previsões de pagamento
                    foreach (var itemPrevisaoPagamento in pagamento.ItensPagamento.Where(item => item.PrevisaoPagamentoEmprestimo != null))
                    {
                        itemPrevisaoPagamento.PrevisaoPagamentoEmprestimo.Realizada = true;
                    }
                    if (repositorioPrevisaoPagamento.Commit().Equals(0))
                        return new OperationResult(StatusCodeEnum.Error, string.Format("Erro ao atualizar previsão de pagamento de empréstimo. Pagamento Id: {0}", pagamento.Id));
                    #endregion

                    // Agrupa os pagamentos por empréstimo
                    var emprestimosPagos = fatura.Itens.Where(it => it.PrevisaoPagamentoEmprestimo != null)
                        .GroupBy(it => it.PrevisaoPagamentoEmprestimo.Emprestimo);

                    var previsoesPagamento = new List<PrevisaoPagamentoEmprestimo>();

                    foreach (var item in emprestimosPagos)
                    {
                        #region Atualiza saldo do empréstimo e quitação
                        var totalPago = item.Sum(it => it.Valor);
                        item.Key.Saldo -= totalPago;

                        if (item.Key.Saldo.Equals(0))
                            item.Key.Quitado = true;
                        #endregion

                        #region Cria histórico de pagamento parcial de um empréstimo
                        if (!item.Key.Quitado)
                        {
                            var pagamentoParcial = new PagamentoParcialEmprestimo
                            {
                                Emprestimo = item.Key,
                                DataPagamento = fatura.DataPagamento,
                                ValorPagamento = totalPago
                            };
                            repositorioPagamentoParcial.Incluir(pagamentoParcial);
                            if (repositorioPagamentoParcial.Commit().Equals(0))
                                return new OperationResult(StatusCodeEnum.Error, string.Format("Erro ao registrar pagamento parcial de empréstimo Id: {0}", item.Key.Id));
                        }
                        #endregion

                        #region Cria a próxima previsão de pagamento se necessário (quando está ocorrendo o pagamento do mês corrente)
                        DateTime dataReferenciaAtual = dataReferencia.HasValue ? dataReferencia.GetValueOrDefault() : new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        DateTime dataReferenciaPagamento = new DateTime(fatura.DataPagamento.Year, fatura.DataPagamento.Month, 1);

                        if (item.Key.Saldo > 0 && dataReferenciaAtual.Equals(dataReferenciaPagamento))
                        {
                            var ultimaPrevisaoPagamento = item.Where(it => it.PrevisaoPagamentoEmprestimo != null && it.PrevisaoPagamentoEmprestimo.Consolidada == false).FirstOrDefault();
                            var proximaPrevisaoPagamento = new PrevisaoPagamentoEmprestimo
                            {
                                Emprestimo = item.Key,
                                DataVencimento = ultimaPrevisaoPagamento.PrevisaoPagamentoEmprestimo.DataVencimento.AddMonths(1),
                                ValorBase = item.Key.Saldo,
                                ValorDevido = item.Key.Saldo * (1 + (Parametrizacao.PercentualJurosEmprestimos / 100)),
                                PercentualJuros = Parametrizacao.PercentualJurosEmprestimos
                            };

                            repositorioPrevisaoPagamento.Incluir(proximaPrevisaoPagamento);
                            if (repositorioPrevisaoPagamento.Commit().Equals(0))
                                return new OperationResult(StatusCodeEnum.Error, string.Format("Erro ao criar nova previsão de pagamento para o saldo do empréstimo Id: {0}", item.Key.Id));

                            // atualiza o saldo do empréstimo com base na nova previsão de pagamento
                            item.Key.Saldo = proximaPrevisaoPagamento.ValorDevido;
                            item.Key.DataProximoVencimento = proximaPrevisaoPagamento.DataVencimento;
                        }
                        #endregion

                        repositorioEmprestimo.Alterar(item.Key);
                        if (repositorioEmprestimo.Commit().Equals(0))
                            return new OperationResult(StatusCodeEnum.Error, string.Format("Erro ao gravar dados do empréstimo Id: {0}", item.Key.Id));
                    }
                }
            }
            catch (Exception ex)
            {
                return new OperationResult(StatusCodeEnum.Error, ex.Message);
            }

            return new OperationResult(pagamento);
        }
        #endregion
    }
}
