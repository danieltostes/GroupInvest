using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Commands.Participantes
{
    public class AlterarParticipanteCommand : ParticipanteCommand
    {
        #region Construtor
        public AlterarParticipanteCommand(int participanteId, string nome, string email, string telefone, string usuarioAplicativoId)
        {
            ParticipanteId = participanteId;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            UsuarioAplicativoId = usuarioAplicativoId;
        }
        #endregion
    }
}
