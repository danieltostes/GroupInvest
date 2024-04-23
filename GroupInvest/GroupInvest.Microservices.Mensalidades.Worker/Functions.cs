using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Microservices.Mensalidades.Application.Events.Adesoes;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Worker
{
    public class Functions
    {
        private readonly IMediatorHandler bus;

        #region Construtor
        public Functions(IMediatorHandler bus)
        {
            this.bus = bus;
        }
        #endregion

        #region Processamento das Filas
        public void ProcessAdesoesQueue([ServiceBusTrigger("adesoes-queue")] string message, ILogger logger)
        {
            var evento = JsonConvert.DeserializeObject<IntegracaoAdesaoRealizadaEvent>(message);
            bus.PublishEvent(evento);
        }
        #endregion
    }
}
