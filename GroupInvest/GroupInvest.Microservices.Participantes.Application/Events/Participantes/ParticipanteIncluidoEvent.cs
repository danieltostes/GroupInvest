using GroupInvest.Common.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Events.Participantes
{
    public class ParticipanteIncluidoEvent : ParticipanteEvent
    {
        public override string EventType => "ParticipanteIncluido";
    }
}
