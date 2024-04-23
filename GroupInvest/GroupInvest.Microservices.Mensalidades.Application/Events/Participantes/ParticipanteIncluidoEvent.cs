using GroupInvest.Common.Application.Events;

namespace GroupInvest.Microservices.Mensalidades.Application.Events.Participantes
{
    public class ParticipanteIncluidoEvent : Event
    {
        public override string EventType => "ParticipanteIncluido";
    }
}
