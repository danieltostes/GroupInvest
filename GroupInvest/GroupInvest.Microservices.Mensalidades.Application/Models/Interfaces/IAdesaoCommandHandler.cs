using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Adesoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para CommandHandler de Adesão
    /// </summary>
    public interface IAdesaoCommandHandler
    {
        OperationResult Handle(RealizarAdesaoCommand command);
    }
}
