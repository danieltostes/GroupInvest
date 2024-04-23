using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Application.Events.Periodos;
using GroupInvest.Microservices.Participantes.Application.Models.Interfaces;
using EventHandler = GroupInvest.Common.Application.EventHandlers.EventHandler;

namespace GroupInvest.Microservices.Participantes.Application.EventHandlers
{
    public class PeriodoEventHandler : EventHandler, IPeriodoEventHandler
    {
        public OperationResult Handle(PeriodoIncluidoEvent evento)
        {
            return OperationResult.OK;
        }

        public OperationResult Handle(PeriodoAlteradoEvent evento)
        {
            return OperationResult.OK;
        }

        public OperationResult Handle(PeriodoEncerradoEvent evento)
        {
            return OperationResult.OK;
        }
    }
}
