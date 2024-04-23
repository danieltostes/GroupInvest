using GroupInvest.Common.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Commands.Participantes
{
    public class CancelarAdesaoParticipanteCommand : Command
    {
        public int ParticipanteId { get; set; }

        public CancelarAdesaoParticipanteCommand(int participanteId)
        {
            this.ParticipanteId = participanteId;
        }
    }
}
