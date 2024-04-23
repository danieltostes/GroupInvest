using GroupInvest.Common.Application.Commands;
using GroupInvest.Common.Application.Events;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GroupInvest.Common.Infrastructure.Messaging
{
    public class AzureServiceBus : IMediatorHandler, IMediatorHandlerQueue
    {
        private readonly string connectionString;

        #region Construtor
        public AzureServiceBus(string connectionString)
        {
            this.connectionString = connectionString;
        }
        #endregion

        #region IMediatorHandler
        public OperationResult PublishEvent<T>(T evento) where T : Event
        {
            try
            {
                if (evento.QueueNames != null && evento.QueueNames.Length > 0)
                {
                    var serializedEvent = JsonConvert.SerializeObject(evento, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    foreach (var queue in evento.QueueNames)
                    {
                        SendMessageAsync(serializedEvent, queue).Wait();
                    }
                }
                return OperationResult.OK;
            }
            catch (Exception ex)
            {
                return new OperationResult(StatusCodeEnum.Error, ex.Message);
            }
        }

        public OperationResult SendCommand<T>(T command) where T : Command
        {
            try
            {
                var serializedCommand = JsonConvert.SerializeObject(command);
                SendMessageAsync(serializedCommand, command.QueueName).Wait();

                return OperationResult.OK;
            }
            catch (Exception ex)
            {
                return new OperationResult(StatusCodeEnum.Error, ex.Message);
            }
        }
        #endregion

        #region Métodos
        private async Task SendMessageAsync(string message, string queueName)
        {
            var queueClient = new QueueClient(connectionString, queueName);
            var encodedMessage = new Message(Encoding.UTF8.GetBytes(message));

            await queueClient.SendAsync(encodedMessage);
            await queueClient.CloseAsync();
        }
        #endregion
    }
}
