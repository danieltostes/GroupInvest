using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Emprestimos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers
{
    /// <summary>
    /// Interface para EventHandler de empréstimos
    /// </summary>
    public interface IEmprestimoEventHandler
    {
        OperationResult Handle(IntegracaoEmprestimoConcedidoEvent evento);
        OperationResult Handle(IntegracaoEmprestimoAtualizacaoSaldoEvent evento);

        OperationResult Handle(EmprestimoConcedidoRegistradoEvent evento);
        OperationResult Handle(SaldosEmprestimosAtualizadosEvent evento);
    }
}
