using AutoMapper;
using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Commands;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Entidades;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Application.Commands.Participantes;
using GroupInvest.Microservices.Participantes.Application.Events;
using GroupInvest.Microservices.Participantes.Application.Events.Participantes;
using GroupInvest.Microservices.Participantes.Application.Models.Interfaces;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Servicos;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.Participantes.Application.CommandHandlers
{
    public class ParticipanteCommandHandler : CommandHandler, IParticipanteCommandHandler
    {
        private readonly IMediatorHandler bus;
        private readonly IMediatorHandlerQueue serviceBus;
        private readonly IServicoParticipante servicoParticipante;
        private readonly IMapper mapper;

        #region Construtor
        public ParticipanteCommandHandler(IMediatorHandler bus, IMediatorHandlerQueue serviceBus, IServicoParticipante servicoParticipante, IMapper mapper)
        {
            this.bus = bus;
            this.serviceBus = serviceBus;
            this.servicoParticipante = servicoParticipante;
            this.mapper = mapper;
        }
        #endregion

        #region Handlers
        public OperationResult Handle(IncluirParticipanteCommand command)
        {
            var participante = mapper.Map<IncluirParticipanteCommand, Participante>(command);
            
            participante.Ativo = true; // participante sempre deve ser cadastrado como ativo

            var result = servicoParticipante.IncluirParticipante(participante);

            if (result.StatusCode == StatusCodeEnum.OK)
            {
                bus.PublishEvent(mapper.Map<Participante, ParticipanteIncluidoEvent>(participante));
                
                var evento = CriarAuditoria(participante, "Inclusão de participante");
                serviceBus.PublishEvent(evento);
            }

            return result;
        }

        public OperationResult Handle(AlterarParticipanteCommand command)
        {
            var participante = servicoParticipante.ObterParticipantePorId(command.ParticipanteId);

            if (participante != null)
            {
                mapper.Map<Command, Participante>(command, participante);
                var result = servicoParticipante.AlterarParticipante(participante);

                if (result.StatusCode == StatusCodeEnum.OK)
                {
                    bus.PublishEvent(mapper.Map<Participante, ParticipanteAlteradoEvent>(participante));

                    var evento = CriarAuditoria(participante, "Alteração de participante");
                    serviceBus.PublishEvent(evento);
                }

                return result;
            }
            else
                return new OperationResult(StatusCodeEnum.Error, "Participante não encontrado para alteração");
        }

        public OperationResult Handle(InativarParticipanteCommand command)
        {
            var participante = servicoParticipante.ObterParticipantePorId(command.ParticipanteId);
            if (participante != null)
            {
                var result = servicoParticipante.InativarParticipante(participante);
                if (result.StatusCode == StatusCodeEnum.OK)
                {
                    var evento = CriarAuditoria(participante, "Inativação de participante");
                    evento.Auditoria.Conteudo = null;

                    bus.PublishEvent(mapper.Map<Participante, ParticipanteInativadoEvent>(participante));
                    serviceBus.PublishEvent(evento);
                }
                return result;
            }
            else
                return new OperationResult(StatusCodeEnum.Error, "Participante não encontrado para inativação");
        }

        public OperationResult Handle(RealizarAdesaoParticipanteCommand command)
        {
            var participante = servicoParticipante.ObterParticipantePorId(command.ParticipanteId);
            if (participante != null)
            {
                var result = servicoParticipante.RealizarAdesaoParticipantePeriodoAtivo(participante, command.NumeroCotas);
                if (result.StatusCode == StatusCodeEnum.OK)
                {
                    var adesao = servicoParticipante.ObterAdesaoAtivaParticipante(participante);
                    if (adesao != null)
                    {
                        var adesaoEvent = mapper.Map<Adesao, AdesaoRealizadaEvent>(adesao);
                        bus.PublishEvent(adesaoEvent);

                        var evento = CriarAuditoria(adesao, "Adesão de participante");
                        evento.Auditoria.Conteudo = JsonConvert.SerializeObject(new
                        {
                            ParticipanteId = adesao.Participante.Id,
                            PeriodoId = adesao.Periodo.Id
                        });

                        serviceBus.PublishEvent(evento);
                        serviceBus.PublishEvent(adesaoEvent);
                    }
                }
                return result;
            }
            else
                return new OperationResult(StatusCodeEnum.Error, "Participante não encontrado para realização da adesão");
        }

        public OperationResult Handle(CancelarAdesaoParticipanteCommand command)
        {
            return new OperationResult(StatusCodeEnum.Error, "Handle de cancelamento de adesão não implementado");
        }
        #endregion

        #region Métodos Privados
        private AlteracaoDadosEvent CriarAuditoria(Entidade<int> entidade, string operacao)
        {
            var evento = new AlteracaoDadosEvent
            {
                Auditoria = new Models.Dtos.AuditoriaDto
                {
                    Agregado = entidade.GetType().FullName,
                    AgregadoId = entidade.Id,
                    Timestamp = DateTime.Now,
                    Operacao = operacao,
                    Conteudo = JsonConvert.SerializeObject(entidade)
                }
            };
            return evento;
        }
        #endregion
    }
}
