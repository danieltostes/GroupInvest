using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.RendimentoParcial;
using GroupInvest.Microservices.AppUsuarios.Application.Events.RendimentoParcial;
using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using EventHandler = GroupInvest.Common.Application.EventHandlers.EventHandler;

namespace GroupInvest.Microservices.AppUsuarios.Application.EventHandlers
{
    public class RendimentoParcialEventHandler : EventHandler, IRendimentoParcialEventHandler
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        #endregion

        #region Construtor
        public RendimentoParcialEventHandler(IMediatorHandler bus)
        {
            this.bus = bus;
        }
        #endregion

        #region IRendimentoParcialEventHandler
        public OperationResult Handle(IntegracaoRendimentoParcialEvent evento)
        {
            var command = new RegistrarRendimentoParcialCommand(evento.DataReferencia, evento.DataAtualizacao, evento.PercentualRendimento);
            return bus.SendCommand(command);
        }

        public OperationResult Handle(RendimentoParcialRegistradoEvent evento)
        {
            return OperationResult.OK;
        }
        #endregion
    }
}
