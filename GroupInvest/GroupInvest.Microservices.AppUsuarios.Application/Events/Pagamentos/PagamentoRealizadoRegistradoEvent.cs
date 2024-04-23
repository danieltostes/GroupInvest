using GroupInvest.Common.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Events.Pagamentos
{
    public class PagamentoRealizadoRegistradoEvent : Event
    {
        public override string EventType => "PagamentoRealizadoRegistrado";
    }
}
