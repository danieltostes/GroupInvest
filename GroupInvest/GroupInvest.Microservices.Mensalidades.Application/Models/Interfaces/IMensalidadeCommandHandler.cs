using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Mensalidades;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para handler de mensalidades
    /// </summary>
    public interface IMensalidadeCommandHandler
    {
        OperationResult Handle(GerarMensalidadesCommand command);
    }
}
