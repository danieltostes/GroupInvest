using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Events.Periodos;
using GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces;
using EventHandler = GroupInvest.Common.Application.EventHandlers.EventHandler;

namespace GroupInvest.Microservices.Mensalidades.Application.EventHandlers
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

        public OperationResult Handle(IntegracaoRendimentoParcialPeriodoEvent evento)
        {
            return OperationResult.OK;
        }
    }
}
