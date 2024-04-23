using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Participantes;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para handler de participante
    /// </summary>
    public interface IParticipanteCommandHandler
    {
        OperationResult Handle(IncluirParticipanteCommand command);
    }
}
