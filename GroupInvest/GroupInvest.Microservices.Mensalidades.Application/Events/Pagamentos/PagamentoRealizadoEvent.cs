using GroupInvest.Common.Application.Events;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Events.Pagamentos
{
    public class PagamentoRealizadoEvent : Event
    {
        public override string EventType => "PagamentoRealizado";

        public Pagamento Pagamento { get; set; }

        public PagamentoRealizadoEvent(Pagamento pagamento)
        {
            this.Pagamento = pagamento;
        }
    }
}
