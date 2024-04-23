using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Events.Adesoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para Handler de eventos de adesão
    /// </summary>
    public interface IAdesaoEventHandler
    {
        OperationResult Handle(AdesaoRealizadaEvent evento);
        OperationResult Handle(IntegracaoAdesaoRealizadaEvent evento);
    }
}
