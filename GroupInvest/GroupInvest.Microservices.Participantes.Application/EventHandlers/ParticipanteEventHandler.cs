using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Application.Events.Participantes;
using GroupInvest.Microservices.Participantes.Application.Models.Interfaces;
using EventHandler = GroupInvest.Common.Application.EventHandlers.EventHandler;

namespace GroupInvest.Microservices.Participantes.Application.EventHandlers
{
    public class ParticipanteEventHandler : EventHandler, IParticipanteEventHandler
    {
        public OperationResult Handle(ParticipanteIncluidoEvent evento)
        {
            return OperationResult.OK;
        }

        public OperationResult Handle(ParticipanteAlteradoEvent evento)
        {
            return OperationResult.OK;
        }

        public OperationResult Handle(ParticipanteInativadoEvent evento)
        {
            return OperationResult.OK;
        }

        public OperationResult Handle(AdesaoRealizadaEvent evento)
        {
            return OperationResult.OK;
        }

        public OperationResult Handle(AdesaoCanceladaEvent evento)
        {
            return OperationResult.OK;
        }
    }
}
