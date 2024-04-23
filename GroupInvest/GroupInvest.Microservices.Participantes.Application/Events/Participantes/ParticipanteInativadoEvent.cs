using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Events.Participantes
{
    public class ParticipanteInativadoEvent : ParticipanteEvent
    {
        public override string EventType => "ParticipanteInativado";
    }
}
