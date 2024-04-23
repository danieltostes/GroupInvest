using AutoMapper;
using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Adesoes;
using GroupInvest.Microservices.Mensalidades.Application.Events.Adesoes;
using GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;

namespace GroupInvest.Microservices.Mensalidades.Application.CommandHandlers
{
    public class AdesaoCommandHandler : CommandHandler, IAdesaoCommandHandler
    {
        #region Injeção de dependência
        private IMediatorHandler bus;
        private readonly IServicoAdesao servicoAdesao;
        private readonly IServicoParticipante servicoParticipante;
        private readonly IServicoPeriodo servicoPeriodo;
        #endregion

        #region Construtor
        public AdesaoCommandHandler(IMediatorHandler bus, IServicoAdesao servicoAdesao, IServicoParticipante servicoParticipante, IServicoPeriodo servicoPeriodo)
        {
            this.bus = bus;
            this.servicoAdesao = servicoAdesao;
            this.servicoParticipante = servicoParticipante;
            this.servicoPeriodo = servicoPeriodo;
        }
        #endregion

        public OperationResult Handle(RealizarAdesaoCommand command)
        {
            var participante = servicoParticipante.ObterParticipantePorId(command.ParticipanteId);
            var periodo = servicoPeriodo.ObterPeriodoPorId(command.PeriodoId);

            var result = servicoAdesao.RealizarAdesao(participante, periodo, command.NumeroCotas);
            
            if (result.StatusCode == StatusCodeEnum.OK)
            {
                var evento = new AdesaoRealizadaEvent
                {
                    ParticipanteId = participante.Id,
                    PeriodoId = periodo.Id,
                    NumeroCotas = command.NumeroCotas
                };
                bus.PublishEvent(evento);
            }

            return result;
        }
    }
}
