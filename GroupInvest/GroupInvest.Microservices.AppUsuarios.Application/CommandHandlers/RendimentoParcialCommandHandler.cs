using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.RendimentoParcial;
using GroupInvest.Microservices.AppUsuarios.Application.Events.RendimentoParcial;
using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.CommandHandlers
{
    public class RendimentoParcialCommandHandler : CommandHandler, IRendimentoParcialCommandHandler
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        private IServicoRendimentoParcialPeriodo servicoRendimentoParcial;
        #endregion

        #region Construtor
        public RendimentoParcialCommandHandler(IMediatorHandler bus, IServicoRendimentoParcialPeriodo servicoRendimentoParcial)
        {
            this.bus = bus;
            this.servicoRendimentoParcial = servicoRendimentoParcial;
        }
        #endregion

        #region IRendimentoParcialCommandHandler
        public OperationResult Handle(RegistrarRendimentoParcialCommand command)
        {
            var rendimento = new RendimentoParcialPeriodo
            {
                DataReferencia = command.DataReferencia,
                DataAtualizacao = command.DataAtualizacao,
                PercentualRendimento = command.PercentualRendimento
            };

            var result = servicoRendimentoParcial.RegistrarRendimentoParcial(rendimento);

            if (result.StatusCode == StatusCodeEnum.OK)
                bus.PublishEvent(new RendimentoParcialRegistradoEvent());

            return result;
        }
        #endregion
    }
}
