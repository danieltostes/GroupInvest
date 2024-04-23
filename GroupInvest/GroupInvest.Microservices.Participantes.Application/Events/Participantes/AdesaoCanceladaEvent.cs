using GroupInvest.Common.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Events.Participantes
{
    public class AdesaoCanceladaEvent : Event
    {
        public override string EventType => "AdesaoCancelada";

        public int ParticipanteId { get; set; }
        public int PeriodoId { get; set; }
    }
}
