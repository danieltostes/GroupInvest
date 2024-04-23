using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GroupInvest.Common.Infrastructure.Messaging
{
    public class AzureServiceBusConsumer
    {
        private string connectionString;
        private IQueueClient queueClient;

        #region Construtor
        public AzureServiceBusConsumer()
        {
            connectionString = "service bus connection string";
        }
        #endregion

        public async Task ProcessQueue(string queueName)
        {
            queueClient = new QueueClient(connectionString, queueName);
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
            await queueClient.CloseAsync();
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs args)
        {
            return Task.CompletedTask;
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            // processa a mensagem
            string messageContent = Encoding.UTF8.GetString(message.Body);

            // informa que a mensagem foi processada com sucesso para que ela seja finalizada e retirada da fila
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }
    }
}
