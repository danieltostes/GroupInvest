using AutoMapper;
using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Participantes;
using GroupInvest.Microservices.Mensalidades.Application.Events.Participantes;
using GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;

namespace GroupInvest.Microservices.Mensalidades.Application.CommandHandlers
{
    public class ParticipanteCommandHandler : CommandHandler, IParticipanteCommandHandler
    {
        private readonly IMediatorHandler bus;
        private readonly IServicoParticipante servicoParticipante;
        private readonly IMapper mapper;

        #region Construtor
        public ParticipanteCommandHandler(IMediatorHandler bus, IServicoParticipante servicoParticipante, IMapper mapper)
        {
            this.bus = bus;
            this.servicoParticipante = servicoParticipante;
            this.mapper = mapper;
        }
        #endregion

        #region IParticipanteCommandHandler
        public OperationResult Handle(IncluirParticipanteCommand command)
        {
            var participante = mapper.Map<IncluirParticipanteCommand, Participante>(command);
            var result = servicoParticipante.IncluirParticipante(participante);

            if (result.StatusCode == StatusCodeEnum.OK)
                bus.PublishEvent(new ParticipanteIncluidoEvent());

            return result;
        }
        #endregion
    }
}
