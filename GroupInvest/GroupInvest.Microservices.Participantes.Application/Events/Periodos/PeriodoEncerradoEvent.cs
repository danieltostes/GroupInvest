using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Events.Periodos
{
    public class PeriodoEncerradoEvent : PeriodoEvent
    {
        public override string EventType => "PeriodoEncerrado";
    }
}
