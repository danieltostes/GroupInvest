using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Microservices.Auditoria.Application.Events;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GroupInvest.Microservices.Auditoria.Worker
{
    public class Functions
    {
        private IMediatorHandler bus;

        #region Construtor
        public Functions(IMediatorHandler bus)
        {
            this.bus = bus;
        }
        #endregion

        #region Processamento das Filas
        public void ProcessEventSourcingQueue([ServiceBusTrigger("event-sourcing-queue")] string message, ILogger logger)
        {
            var evento = JsonConvert.DeserializeObject<AlteracaoDadosEvent>(message);
            bus.PublishEvent(evento);
        }
        #endregion
    }
}
