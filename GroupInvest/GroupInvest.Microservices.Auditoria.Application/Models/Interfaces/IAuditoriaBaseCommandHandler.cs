using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Auditoria.Application.Commands.AuditoriaBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Auditoria.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para handler de auditoria base
    /// </summary>
    public interface IAuditoriaBaseCommandHandler
    {
        OperationResult Handle(IncluirAuditoriaBaseCommand command);
    }
}
