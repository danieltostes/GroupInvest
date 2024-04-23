using GroupInvest.Common.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Events.Periodos
{
    public class PeriodoIncluidoEvent : Event
    {
        public override string EventType => "PeriodoIncluido";
    }
}
