using GroupInvest.Common.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Commands.Participantes
{
    public abstract class ParticipanteCommand : Command
    {
        public int ParticipanteId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string UsuarioAplicativoId { get; set; }
    }
}
