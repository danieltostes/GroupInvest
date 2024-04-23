using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Events.Periodos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para o handler de eventos de período
    /// </summary>
    public interface IPeriodoEventHandler
    {
        OperationResult Handle(PeriodoIncluidoEvent evento);
        OperationResult Handle(PeriodoAlteradoEvent evento);
        OperationResult Handle(PeriodoEncerradoEvent evento);
        OperationResult Handle(IntegracaoRendimentoParcialPeriodoEvent evento);
    }
}
