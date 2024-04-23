using GroupInvest.Common.Application.Events;
using GroupInvest.Microservices.Mensalidades.Application.Models.VO;
using System.Collections.Generic;

namespace GroupInvest.Microservices.Mensalidades.Application.Events.Mensalidades
{
    public class MensalidadesGeradasEvent : Event
    {
        public override string EventType => "MensalidadesGeradas";
        public override string[] QueueNames => new string[] { "mensalidades-queue" };

        public int ParticipanteId { get; set; }
        public string Nome { get; set; }

        public IReadOnlyCollection<MensalidadeVO> Mensalidades { get; set; }
    }
}
