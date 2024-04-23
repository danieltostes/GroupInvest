using AutoMapper;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Pagamentos;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Pagamentos;
using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using EventHandler = GroupInvest.Common.Application.EventHandlers.EventHandler;

namespace GroupInvest.Microservices.AppUsuarios.Application.EventHandlers
{
    public class PagamentoEventHandler : EventHandler, IPagamentoEventHandler
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        private readonly IMapper mapper;
        #endregion

        #region Construtor
        public PagamentoEventHandler(IMediatorHandler bus, IMapper mapper)
        {
            this.bus = bus;
            this.mapper = mapper;
        }
        #endregion

        #region IPagamentoEventHandler
        public OperationResult Handle(IntegracaoPagamentoRealizadoEvent evento)
        {
            var command = mapper.Map<IntegracaoPagamentoRealizadoEvent, RegistrarPagamentoCommand>(evento);
            var result = bus.SendCommand(command);

            return result;
        }

        public OperationResult Handle(PagamentoRealizadoRegistradoEvent evento)
        {
            return OperationResult.OK;
        }
        #endregion
    }
}
