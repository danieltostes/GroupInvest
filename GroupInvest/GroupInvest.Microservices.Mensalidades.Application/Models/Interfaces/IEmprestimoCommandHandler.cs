using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Emprestimos;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para CommandHandler de Empréstimos
    /// </summary>
    public interface IEmprestimoCommandHandler
    {
        OperationResult Handle(ConcederEmprestimoCommand command);
        OperationResult Handle(AtualizarPrevisoesPagamentoEmprestimosCommand command);
    }
}
