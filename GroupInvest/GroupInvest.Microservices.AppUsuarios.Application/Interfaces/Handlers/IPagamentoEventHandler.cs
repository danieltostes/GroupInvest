using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Pagamentos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers
{
    /// <summary>
    /// Interface para EventHandler de pagamentos
    /// </summary>
    public interface IPagamentoEventHandler
    {
        OperationResult Handle(IntegracaoPagamentoRealizadoEvent evento);
        OperationResult Handle(PagamentoRealizadoRegistradoEvent evento);
    }
}
