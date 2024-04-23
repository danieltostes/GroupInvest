using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Periodos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para handler de período
    /// </summary>
    public interface IPeriodoCommandHandler
    {
        OperationResult Handle(IncluirPeriodoCommand command);
        OperationResult Handle(AlterarPeriodoCommand command);
        OperationResult Handle(EncerrarPeriodoCommand command);
        OperationResult Handle(CalcularRendimentoParcialPeriodoCommand command);
    }
}
