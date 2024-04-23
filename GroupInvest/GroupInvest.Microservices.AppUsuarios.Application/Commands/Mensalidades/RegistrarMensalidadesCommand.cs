using GroupInvest.Common.Application.Commands;
using GroupInvest.Microservices.AppUsuarios.Application.Models.VO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Commands.Mensalidades
{
    public class RegistrarMensalidadesCommand : Command
    {
        public int ParticipanteId { get; set; }
        public string Nome { get; set; }

        public List<MensalidadeVO> Mensalidades { get; set; }
    }
}
