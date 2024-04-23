using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Emprestimos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers
{
    /// <summary>
    /// Interface para CommandHandler de empréstimos
    /// </summary>
    public interface IEmprestimoCommandHandler
    {
        OperationResult Handle(RegistrarEmprestimoConcedidoCommand command);
        OperationResult Handle(RegistrarAtualizacaoSaldoEmprestimosCommand command);
    }
}
