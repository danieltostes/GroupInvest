using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Events.Periodos
{
    public class PeriodoIncluidoEvent : PeriodoEvent
    {
        public override string EventType => "PeriodoIncluido";
    }
}
