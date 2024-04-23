using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Pagamentos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para CommandHandler de Pagamentos
    /// </summary>
    public interface IPagamentoCommandHandler
    {
        OperationResult Handle(RealizarPagamentoCommand command);
        OperationResult Handle(RealizarPagamentoRetroativoCommand command);
    }
}
