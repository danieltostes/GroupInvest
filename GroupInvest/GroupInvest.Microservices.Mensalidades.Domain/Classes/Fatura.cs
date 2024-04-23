using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Classes
{
    public class Fatura : IFatura
    {
        private List<ItemPagamento> itens;
        private decimal valorTotal;

        public DateTime DataPagamento { get; }
        public decimal ValorTotal { get => valorTotal; }
        public IReadOnlyCollection<ItemPagamento> Itens { get => itens; }

        #region Construtor
        public Fatura(DateTime dataPagamento)
        {
            this.DataPagamento = dataPagamento;
            this.itens = new List<ItemPagamento>();
        }
        #endregion

        public void AdicionarMensalidade(Mensalidade mensalidade)
        {
            if (mensalidade.DataPagamento.HasValue)
                return;

            if (mensalidade.DataVencimento.GetValueOrDefault() < this.DataPagamento)
            {
                mensalidade.PercentualJuros = Parametrizacao.PercentualJurosMensalidades;
                mensalidade.ValorDevido = mensalidade.ValorBase * (1 + (Parametrizacao.PercentualJurosMensalidades / 100));
            }

            var valorPagamento = mensalidade.ValorDevido;

            var itemPagamento = new ItemPagamento
            {
                Mensalidade = mensalidade,
                Valor = valorPagamento
            };

            valorTotal += valorPagamento;

            itens.Add(itemPagamento);
        }

        public void AdicionarPrevisaoPagamentoEmprestimo(PrevisaoPagamentoEmprestimo previsaoPagamento, decimal valorPagamento)
        {
            if (previsaoPagamento.Realizada)
                return;

            if (previsaoPagamento.Consolidada && valorPagamento != previsaoPagamento.ValorDevido)
                throw new Exception("Para previsões de empréstimo consolidadas, não é possivel realizar um pagamento com valor diferente do valor devido");

            if (!previsaoPagamento.Consolidada && valorPagamento > previsaoPagamento.ValorDevido)
                throw new Exception("O valor do pagamento não pode ser superior ao valor da previsão de pagamento.");

            var itemPagamento = new ItemPagamento
            {
                PrevisaoPagamentoEmprestimo = previsaoPagamento,
                Valor = valorPagamento
            };

            valorTotal += itemPagamento.Valor;
            itens.Add(itemPagamento);
        }
    }
}
