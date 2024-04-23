using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Events.Emprestimos;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para EventHandler de Empréstimos
    /// </summary>
    public interface IEmprestimoEventHandler
    {
        OperationResult Handle(EmprestimoConcedidoEvent evento);
        OperationResult Handle(PrevisoesPagamentoEmprestimosAtualizadasEvent evento);
    }
}
