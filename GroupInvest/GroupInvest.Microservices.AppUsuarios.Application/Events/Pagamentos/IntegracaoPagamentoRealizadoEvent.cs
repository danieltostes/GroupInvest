using GroupInvest.Common.Application.Events;
using GroupInvest.Microservices.AppUsuarios.Application.Models.VO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Events.Pagamentos
{
    public class IntegracaoPagamentoRealizadoEvent : Event
    {
        public override string EventType => "IntegracaoPagamentoRealizado";

        public int Id { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorTotalPagamento { get; set; }

        public List<ItemPagamentoVO> ItensPagamento { get; set; }
    }
}
