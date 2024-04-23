using GroupInvest.Common.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Commands.Participantes
{
    public class RealizarAdesaoParticipanteCommand : Command
    {
        public int ParticipanteId { get; set; }
        public int NumeroCotas { get; set; }

        public RealizarAdesaoParticipanteCommand(int participanteId, int numeroCotas)
        {
            this.ParticipanteId = participanteId;
            this.NumeroCotas = numeroCotas;
        }
    }
}
