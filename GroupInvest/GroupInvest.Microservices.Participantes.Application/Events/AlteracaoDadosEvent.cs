using GroupInvest.Common.Application.Events;
using GroupInvest.Microservices.Participantes.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Events
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
