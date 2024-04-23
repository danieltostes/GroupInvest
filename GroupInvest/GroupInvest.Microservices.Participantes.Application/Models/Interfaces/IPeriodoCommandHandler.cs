using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Application.Commands.Periodos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para CommandHandler de Período
    /// </summary>
    public interface IPeriodoCommandHandler
    {
        OperationResult Handle(IncluirPeriodoCommand command);
        OperationResult Handle(AlterarPeriodoCommand command);
        OperationResult Handle(EncerrarPeriodoCommand command);
    }
}
