using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Commands.Participantes
{
    public class InativarParticipanteCommand : ParticipanteCommand
    {
        #region Construtor
        public InativarParticipanteCommand(int id)
        {
            this.ParticipanteId = id;
        }
        #endregion
    }
}
