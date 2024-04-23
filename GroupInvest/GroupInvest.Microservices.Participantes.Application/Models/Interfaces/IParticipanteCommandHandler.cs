using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Application.Commands.Participantes;

namespace GroupInvest.Microservices.Participantes.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para handler de participantes
    /// </summary>
    public interface IParticipanteCommandHandler
    {
        OperationResult Handle(IncluirParticipanteCommand command);
        OperationResult Handle(AlterarParticipanteCommand command);
        OperationResult Handle(InativarParticipanteCommand command);
        OperationResult Handle(RealizarAdesaoParticipanteCommand command);
        OperationResult Handle(CancelarAdesaoParticipanteCommand command);
    }
}
