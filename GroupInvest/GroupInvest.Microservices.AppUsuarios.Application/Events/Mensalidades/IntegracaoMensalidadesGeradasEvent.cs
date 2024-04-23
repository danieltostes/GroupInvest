using GroupInvest.Common.Application.Events;
using GroupInvest.Microservices.AppUsuarios.Application.Models.VO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Events.Mensalidades
{
    public class IntegracaoMensalidadesGeradasEvent : Event
    {
        public override string EventType => "IntegracaoMensalidadesGeradas";

        public int ParticipanteId { get; set; }
        public string Nome { get; set; }

        public List<MensalidadeVO> Mensalidades { get; set; }
    }
}
