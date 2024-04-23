using Event = GroupInvest.Common.Application.Events.Event;

namespace GroupInvest.Microservices.Mensalidades.Application.Events.Adesoes
{
    public class AdesaoRealizadaEvent : Event
    {
        public override string EventType => "AdesaoIncluida";

        public int ParticipanteId { get; set; }
        public int PeriodoId { get; set; }
        public int NumeroCotas { get; set; }
    }
}
