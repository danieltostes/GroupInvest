using AutoMapper;
using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Application.Commands.Periodos;
using GroupInvest.Microservices.Participantes.Application.Events;
using GroupInvest.Microservices.Participantes.Application.Events.Periodos;
using GroupInvest.Microservices.Participantes.Application.Models.Interfaces;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Servicos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.CommandHandlers
{
    public class PeriodoCommandHandler : CommandHandler, IPeriodoCommandHandler
    {
        private readonly IMediatorHandler bus;
        private readonly IMediatorHandlerQueue serviceBus;
        private readonly IServicoPeriodo servicoPeriodo;
        private readonly IMapper mapper;

        #region Construtor
        public PeriodoCommandHandler(IMediatorHandler bus, IMediatorHandlerQueue serviceBus, IServicoPeriodo servicoPeriodo, IMapper mapper)
        {
            this.bus = bus;
            this.serviceBus = serviceBus;
            this.servicoPeriodo = servicoPeriodo;
            this.mapper = mapper;
        }
        #endregion

        #region IPeriodoCommandHandler
        public OperationResult Handle(IncluirPeriodoCommand command)
        {
            var periodo = mapper.Map<IncluirPeriodoCommand, Periodo>(command);
            var result = servicoPeriodo.IncluirPeriodo(periodo);

            if (result.StatusCode == StatusCodeEnum.OK)
            {
                bus.PublishEvent(mapper.Map<Periodo, PeriodoIncluidoEvent>(periodo));
                serviceBus.PublishEvent(CriarAuditoria(periodo, "Inclusão de Período"));
            }

            return result;
        }

        public OperationResult Handle(AlterarPeriodoCommand command)
        {
            var periodo = servicoPeriodo.ObterPeriodoPorAnoReferencia(command.AnoReferencia);

            if (periodo != null)
            {
                mapper.Map<AlterarPeriodoCommand, Periodo>(command, periodo);
                var result = servicoPeriodo.AlterarPeriodo(periodo);
                if (result.StatusCode == StatusCodeEnum.OK)
                {
                    bus.PublishEvent(mapper.Map<Periodo, PeriodoAlteradoEvent>(periodo));
                    serviceBus.PublishEvent(CriarAuditoria(periodo, "Alteração de Período"));
                }
                return result;
            }
            else
                return new OperationResult(StatusCodeEnum.Error, "Período não encontrado para alteração");
        }

        public OperationResult Handle(EncerrarPeriodoCommand command)
        {
            var periodo = servicoPeriodo.ObterPeriodoPorAnoReferencia(command.AnoReferencia);

            if (periodo != null)
            {
                var result = servicoPeriodo.EncerrarPeriodo(periodo);
                if (result.StatusCode == StatusCodeEnum.OK)
                {
                    var evento = CriarAuditoria(periodo, "Encerramento de Período");
                    evento.Auditoria.Conteudo = null;

                    bus.PublishEvent(mapper.Map<Periodo, PeriodoEncerradoEvent>(periodo));
                    serviceBus.PublishEvent(evento);
                }
                return result;
            }
            else
                return new OperationResult(StatusCodeEnum.Error, "Período não encontrado para encerramento");
        }
        #endregion

        #region Métodos Privados
        private AlteracaoDadosEvent CriarAuditoria(Periodo periodo, string operacao)
        {
            var evento = new AlteracaoDadosEvent
            {
                Auditoria = new Models.Dtos.AuditoriaDto
                {
                    Agregado = periodo.GetType().FullName,
                    AgregadoId = periodo.Id,
                    Timestamp = DateTime.Now,
                    Operacao = operacao,
                    Conteudo = JsonConvert.SerializeObject(periodo)
                }
            };
            return evento;
        }
        #endregion
    }
}
