using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Transacoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers
{
    public interface ITransacaoEventHandler
    {
        OperationResult Handle(IntegracaoTransacaoRealizadaEvent evento);
        OperationResult Handle(TransacaoRegistradaEvent evento);
    }
}
