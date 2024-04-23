using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Events.Participantes;
using GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces;
using EventHandler = GroupInvest.Common.Application.EventHandlers.EventHandler;

namespace GroupInvest.Microservices.Mensalidades.Application.EventHandlers
{
    public class ParticipanteEventHandler : EventHandler, IParticipanteEventHandler
    {
        public OperationResult Handle(ParticipanteIncluidoEvent evento)
        {
            return OperationResult.OK;
        }
    }
}
