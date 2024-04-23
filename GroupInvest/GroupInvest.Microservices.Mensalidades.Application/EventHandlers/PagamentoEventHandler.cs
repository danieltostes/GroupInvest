using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Events.Pagamentos;
using GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using EventHandler = GroupInvest.Common.Application.EventHandlers.EventHandler;

namespace GroupInvest.Microservices.Mensalidades.Application.EventHandlers
{
    public class PagamentoEventHandler : EventHandler, IPagamentoEventHandler
    {
        #region IPagamentoEventHandler
        public OperationResult Handle(PagamentoRealizadoEvent evento)
        {
            return OperationResult.OK;
        }

        public OperationResult Handle(PagamentoRetroativoRealizadoEvent evento)
        {
            return OperationResult.OK;
        }
        #endregion
    }
}
