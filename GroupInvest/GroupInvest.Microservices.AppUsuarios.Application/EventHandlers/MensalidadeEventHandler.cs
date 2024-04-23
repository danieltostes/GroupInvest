using AutoMapper;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Mensalidades;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Mensalidades;
using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using EventHandler = GroupInvest.Common.Application.EventHandlers.EventHandler;

namespace GroupInvest.Microservices.AppUsuarios.Application.EventHandlers
{
    public class MensalidadeEventHandler : EventHandler, IMensalidadeEventHandler
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        private readonly IMapper mapper;
        #endregion

        #region Construtor
        public MensalidadeEventHandler(IMediatorHandler bus, IMapper mapper)
        {
            this.bus = bus;
            this.mapper = mapper;
        }
        #endregion

        #region IMensalidadeEventHandler
        public OperationResult Handle(IntegracaoMensalidadesGeradasEvent evento)
        {
            var command = mapper.Map<IntegracaoMensalidadesGeradasEvent, RegistrarMensalidadesCommand>(evento);
            var result = bus.SendCommand(command);

            return result;
        }

        public OperationResult Handle(MensalidadesRegistradasEvent evento)
        {
            return OperationResult.OK;
        }
        #endregion
    }
}
