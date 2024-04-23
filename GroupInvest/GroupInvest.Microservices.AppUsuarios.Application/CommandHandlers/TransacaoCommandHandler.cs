using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Transacoes;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Transacoes;
using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.CommandHandlers
{
    public class TransacaoCommandHandler : CommandHandler, ITransacaoCommandHandler
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        private readonly IServicoTransacao servicoTransacao;
        #endregion

        #region Construtor
        public TransacaoCommandHandler(IMediatorHandler bus, IServicoTransacao servicoTransacao)
        {
            this.bus = bus;
            this.servicoTransacao = servicoTransacao;
        }
        #endregion

        #region ITransacaoCommandHandler
        public OperationResult Handle(RegistrarTransacaoCommand command)
        {
            var transacao = new Transacao
            {
                ParticipanteId = command.Transacao.ParticipanteId,
                CodigoTransacao = command.Transacao.CodigoTransacao,
                DataTransacao = command.Transacao.DataTransacao,
                Descricao = command.Transacao.Descricao,
                ValorTransacao = command.Transacao.ValorTransacao
            };

            var result = servicoTransacao.IncluirTransacao(transacao);

            if (result.StatusCode == StatusCodeEnum.OK)
                bus.PublishEvent(new TransacaoRegistradaEvent());

            return result;
        }
        #endregion
    }
}
