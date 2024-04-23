using AutoMapper;
using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Mensalidades;
using GroupInvest.Microservices.Mensalidades.Application.Events.Mensalidades;
using GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces;
using GroupInvest.Microservices.Mensalidades.Application.Models.VO;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using System.Collections.Generic;
using System.Linq;

namespace GroupInvest.Microservices.Mensalidades.Application.CommandHandlers
{
    public class MensalidadeCommandHandler : CommandHandler, IMensalidadeCommandHandler
    {
        private readonly IMediatorHandler bus;
        private readonly IMediatorHandlerQueue serviceBus;
        private readonly IMapper mapper;
        private readonly IServicoMensalidade servicoMensalidade;
        private readonly IServicoAdesao servicoAdesao;

        #region Construtor
        public MensalidadeCommandHandler(IMediatorHandler bus, IMediatorHandlerQueue serviceBus, IMapper mapper, IServicoMensalidade servicoMensalidade, IServicoAdesao servicoAdesao)
        {
            this.bus = bus;
            this.serviceBus = serviceBus;
            this.mapper = mapper;
            this.servicoMensalidade = servicoMensalidade;
            this.servicoAdesao = servicoAdesao;
        }
        #endregion

        #region IMensalidadeCommandHandler
        public OperationResult Handle(GerarMensalidadesCommand command)
        {
            var adesao = servicoAdesao.ObterAdesaoPorId(command.AdesaoId);

            if (adesao == null)
                return new OperationResult(StatusCodeEnum.Error,
                    string.Format("Adesão não encontrada: Id {0}", command.AdesaoId));

            var result = servicoMensalidade.GerarMensalidades(adesao);

            if (result.StatusCode == StatusCodeEnum.OK)
            {
                var mensalidades = (IReadOnlyCollection<Mensalidade>)result.ObjectResult;
                var evento = new MensalidadesGeradasEvent
                {
                    ParticipanteId = mensalidades.ElementAt(0).Adesao.Participante.Id,
                    Nome = mensalidades.ElementAt(0).Adesao.Participante.Nome
                };

                var mensalidadesVO = new List<MensalidadeVO>();
                foreach (var mensalidade in mensalidades)
                    mensalidadesVO.Add(mapper.Map<Mensalidade, MensalidadeVO>(mensalidade));

                evento.Mensalidades = mensalidadesVO;

                serviceBus.PublishEvent(evento);
                bus.PublishEvent(evento);
            }

            return result;
        }
        #endregion
    }
}
