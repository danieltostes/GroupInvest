using AutoMapper;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Adesoes;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Mensalidades;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Participantes;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Periodos;
using GroupInvest.Microservices.Mensalidades.Application.Events.Adesoes;
using GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using EventHandler = GroupInvest.Common.Application.EventHandlers.EventHandler;

namespace GroupInvest.Microservices.Mensalidades.Application.EventHandlers
{
    public class AdesaoEventHandler : EventHandler, IAdesaoEventHandler
    {
        private readonly IMediatorHandler bus;
        private readonly IMapper mapper;
        private readonly IServicoPeriodo servicoPeriodo;
        private readonly IServicoParticipante servicoParticipante;
        private readonly IServicoAdesao servicoAdesao;

        public AdesaoEventHandler(IMediatorHandler bus, IMapper mapper, IServicoPeriodo servicoPeriodo, IServicoParticipante servicoParticipante, IServicoAdesao servicoAdesao)
        {
            this.bus = bus;
            this.mapper = mapper;
            this.servicoPeriodo = servicoPeriodo;
            this.servicoParticipante = servicoParticipante;
            this.servicoAdesao = servicoAdesao;
        }

        public OperationResult Handle(IntegracaoAdesaoRealizadaEvent evento)
        {
            OperationResult result;

            // verifica se o período já foi cadastrado
            var periodo = servicoPeriodo.ObterPeriodoPorId(evento.Periodo.Id);
            if (periodo == null)
            {
                // inclui o período na base de dados caso seja um período novo
                var incluirPeriodoCommand = mapper.Map<IntegracaoAdesaoRealizadaEvent, IncluirPeriodoCommand>(evento);
                result = bus.SendCommand(incluirPeriodoCommand);
                if (result.StatusCode == StatusCodeEnum.Error)
                    return result;
            }

            // verifica se o participante já foi cadastrado
            var participante = servicoParticipante.ObterParticipantePorId(evento.Participante.Id);
            if (participante == null)
            {
                // inclui o participante na base de dados caso seja um participante novo
                var incluirParticipanteCommand = mapper.Map<IntegracaoAdesaoRealizadaEvent, IncluirParticipanteCommand>(evento);
                result = bus.SendCommand(incluirParticipanteCommand);
                if (result.StatusCode == StatusCodeEnum.Error)
                    return result;
            }

            // Inclui a adesão do participante
            var realizarAdesaoCommand = new RealizarAdesaoCommand(evento.Participante.Id, evento.Periodo.Id, evento.NumeroCotas);
            result = bus.SendCommand(realizarAdesaoCommand);

            return result;
        }

        public OperationResult Handle(AdesaoRealizadaEvent evento)
        {
            var adesao = servicoAdesao.ObterAdesao(evento.ParticipanteId, evento.PeriodoId);

            if (adesao != null)
            {
                var command = new GerarMensalidadesCommand(adesao.Id);
                return bus.SendCommand(command);
            }
            else return new OperationResult(StatusCodeEnum.Error, string.Format("Adesão não encontrada. ParticipanteId: {0} PeriodoId: {1}", evento.ParticipanteId, evento.PeriodoId));
        }
    }
}
