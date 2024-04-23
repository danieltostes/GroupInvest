using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Events.Pagamentos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para EventHandler de Pagamentos
    /// </summary>
    public interface IPagamentoEventHandler
    {
        OperationResult Handle(PagamentoRealizadoEvent evento);
        OperationResult Handle(PagamentoRetroativoRealizadoEvent evento);
    }
}
