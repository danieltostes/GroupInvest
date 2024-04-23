using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Auditoria.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Auditoria.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para handler de eventos de auditoria
    /// </summary>
    public interface IAuditoriaBaseEventHandler
    {
        OperationResult Handle(RegistroAuditoriaBaseIncluidoEvent evento);
        OperationResult Handle(AlteracaoDadosEvent evento);
    }
}
