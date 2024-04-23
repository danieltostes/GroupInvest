using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Pagamentos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers
{
    /// <summary>
    /// Interface para CommandHandler de pagamentos
    /// </summary>
    public interface IPagamentoCommandHandler
    {
        OperationResult Handle(RegistrarPagamentoCommand command);
    }
}
