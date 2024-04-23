using GroupInvest.Common.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Events.Participantes
{
    public class ParticipanteAlteradoEvent : ParticipanteEvent
    {
        public override string EventType => "ParticipanteAlterado";
    }
}
