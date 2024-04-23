using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Transacoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers
{
    public interface ITransacaoCommandHandler
    {
        OperationResult Handle(RegistrarTransacaoCommand command);
    }
}
