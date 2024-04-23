using GroupInvest.Common.Application.Events;
using GroupInvest.Microservices.Mensalidades.Application.Models.VO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Events.Pagamentos
{
    public class IntegracaoPagamentoRealizadoEvent : Event
    {
        public override string EventType => "IntegracaoPagamento";
        public override string[] QueueNames => new string[] { "pagamentos-queue" };

        public int Id { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorTotalPagamento { get; set; }
        public List<ItemPagamentoVO> ItensPagamento { get; set; }

        public IntegracaoPagamentoRealizadoEvent()
        {
            ItensPagamento = new List<ItemPagamentoVO>();
        }
    }
}
