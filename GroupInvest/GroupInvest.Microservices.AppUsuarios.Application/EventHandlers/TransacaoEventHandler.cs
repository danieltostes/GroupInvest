using AutoMapper;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Transacoes;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Transacoes;
using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers;
using GroupInvest.Microservices.AppUsuarios.Application.Models.VO;
using System;
using System.Collections.Generic;
using System.Text;
using EventHandler = GroupInvest.Common.Application.EventHandlers.EventHandler;

namespace GroupInvest.Microservices.AppUsuarios.Application.EventHandlers
{
    public class TransacaoEventHandler : EventHandler, ITransacaoEventHandler
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        private readonly IMapper mapper;
        #endregion

        #region Construtor
        public TransacaoEventHandler(IMediatorHandler bus, IMapper mapper)
        {
            this.bus = bus;
            this.mapper = mapper;
        }
        #endregion

        public OperationResult Handle(IntegracaoTransacaoRealizadaEvent evento)
        {
            var command = new RegistrarTransacaoCommand(mapper.Map<IntegracaoTransacaoRealizadaEvent, TransacaoVO>(evento));
            return bus.SendCommand(command);
        }

        public OperationResult Handle(TransacaoRegistradaEvent evento)
        {
            return OperationResult.OK;
        }
    }
}
