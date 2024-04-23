using AutoMapper;
using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Mensalidades;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Mensalidades;
using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.CommandHandlers
{
    public class MensalidadeCommandHandler : CommandHandler, IMensalidadeCommandHandler
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        private readonly IMapper mapper;
        private readonly IServicoMensalidade servicoMensalidade;
        #endregion

        #region Construtor
        public MensalidadeCommandHandler(IMediatorHandler bus, IMapper mapper, IServicoMensalidade servicoMensalidade)
        {
            this.bus = bus;
            this.mapper = mapper;
            this.servicoMensalidade = servicoMensalidade;
        }
        #endregion

        #region IMensalidadeCommandHandler
        public OperationResult Handle(RegistrarMensalidadesCommand command)
        {
            foreach (var vo in command.Mensalidades)
            {
                var mensalidade = new Mensalidade
                {
                    MensalidadeId = vo.Id,
                    ParticipanteId = command.ParticipanteId,
                    NomeParticipante = command.Nome,
                    DataReferencia = vo.DataReferencia.GetValueOrDefault(),
                    DataVencimento = vo.DataVencimento.GetValueOrDefault(),
                    ValorBase = vo.ValorBase,
                    ValorDevido = vo.ValorDevido
                };

                servicoMensalidade.IncluirMensalidade(mensalidade);
            }

            bus.PublishEvent(new MensalidadesRegistradasEvent());
            return OperationResult.OK;
        }
        #endregion
    }
}
