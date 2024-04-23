using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Application.Events.Periodos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para EventHandler de Periodo
    /// </summary>
    public interface IPeriodoEventHandler
    {
        OperationResult Handle(PeriodoIncluidoEvent evento);
        OperationResult Handle(PeriodoAlteradoEvent evento);
        OperationResult Handle(PeriodoEncerradoEvent evento);
    }
}
