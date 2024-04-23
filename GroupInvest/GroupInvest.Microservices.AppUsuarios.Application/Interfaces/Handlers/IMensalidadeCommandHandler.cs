using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Mensalidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers
{
    /// <summary>
    /// Interface para CommandHandler de mensalidades
    /// </summary>
    public interface IMensalidadeCommandHandler
    {
        OperationResult Handle(RegistrarMensalidadesCommand command);
    }
}
