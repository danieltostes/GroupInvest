using GroupInvest.Common.Application.Events;
using GroupInvest.Microservices.Mensalidades.Application.Models.Dtos;

namespace GroupInvest.Microservices.Mensalidades.Application.Events
{
    public class AlteracaoDadosEvent : Event
    {
        public override string[] QueueNames => new string[] { "event-sourcing-queue" };
        public override string EventType => "AlteracaoDados";

        #region Propriedades
        public AuditoriaDto Auditoria { get; set; }
        #endregion
    }
}
