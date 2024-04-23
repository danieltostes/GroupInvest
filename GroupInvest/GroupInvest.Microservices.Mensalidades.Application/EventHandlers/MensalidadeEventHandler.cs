using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Events.Mensalidades;
using GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces;
using EventHandler = GroupInvest.Common.Application.EventHandlers.EventHandler;

namespace GroupInvest.Microservices.Mensalidades.Application.EventHandlers
{
    public class MensalidadeEventHandler : EventHandler, IMensalidadeEventHandler
    {
        public OperationResult Handle(MensalidadesGeradasEvent evento)
        {
            return OperationResult.OK;
        }
    }
}
