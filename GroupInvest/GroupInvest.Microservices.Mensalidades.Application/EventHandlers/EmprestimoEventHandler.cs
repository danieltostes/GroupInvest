using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Events.Emprestimos;
using GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces;
using EventHandler = GroupInvest.Common.Application.EventHandlers.EventHandler;

namespace GroupInvest.Microservices.Mensalidades.Application.EventHandlers
{
    public class EmprestimoEventHandler : EventHandler, IEmprestimoEventHandler
    {
        public OperationResult Handle(EmprestimoConcedidoEvent evento)
        {
            return OperationResult.OK;
        }

        public OperationResult Handle(PrevisoesPagamentoEmprestimosAtualizadasEvent evento)
        {
            return OperationResult.OK;
        }
    }
}
