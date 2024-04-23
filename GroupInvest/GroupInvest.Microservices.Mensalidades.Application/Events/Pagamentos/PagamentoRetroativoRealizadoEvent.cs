using GroupInvest.Common.Application.Events;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Events.Pagamentos
{
    public class PagamentoRetroativoRealizadoEvent : Event
    {
        public override string EventType => "PagamentoRetroativoRealizado";

        public Pagamento Pagamento { get; set; }

        public PagamentoRetroativoRealizadoEvent(Pagamento pagamento)
        {
            this.Pagamento = pagamento;
        }
    }
}
