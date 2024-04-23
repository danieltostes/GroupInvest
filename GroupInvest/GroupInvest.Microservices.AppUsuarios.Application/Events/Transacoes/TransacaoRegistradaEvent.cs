using GroupInvest.Common.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Events.Transacoes
{
    public class TransacaoRegistradaEvent : Event
    {
        public override string EventType => "TransacaoRegistrada";
    }
}
