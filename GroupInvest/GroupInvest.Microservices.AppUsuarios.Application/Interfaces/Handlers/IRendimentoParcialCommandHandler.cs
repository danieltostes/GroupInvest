using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.RendimentoParcial;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers
{
    /// <summary>
    /// Interface para CommandHandler de Rendimento Parcial
    /// </summary>
    public interface IRendimentoParcialCommandHandler
    {
        OperationResult Handle(RegistrarRendimentoParcialCommand command);
    }
}
