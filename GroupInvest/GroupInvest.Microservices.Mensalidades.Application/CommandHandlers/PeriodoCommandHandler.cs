using AutoMapper;
using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Periodos;
using GroupInvest.Microservices.Mensalidades.Application.Events;
using GroupInvest.Microservices.Mensalidades.Application.Events.Periodos;
using GroupInvest.Microservices.Mensalidades.Application.Models.Dtos;
using GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using Newtonsoft.Json;
using System;

namespace GroupInvest.Microservices.Mensalidades.Application.CommandHandlers
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
                bus.PublishEvent(new PeriodoIncluidoEvent());

            return result;
        }

        public OperationResult Handle(AlterarPeriodoCommand command)
        {
            var periodo = mapper.Map<AlterarPeriodoCommand, Periodo>(command);
            var result = servicoPeriodo.AlterarPeriodo(periodo);

            if (result.StatusCode == StatusCodeEnum.OK)
                bus.PublishEvent(new PeriodoAlteradoEvent());

            return result;
        }

        public OperationResult Handle(EncerrarPeriodoCommand command)
        {
            var periodo = servicoPeriodo.ObterPeriodoPorId(command.PeriodoId);
            var result = servicoPeriodo.EncerrarPeriodo(periodo);

            if (result.StatusCode == StatusCodeEnum.OK)
            {
                var evento = new PeriodoEncerradoEvent { DistribuicaoCotas = (DistribuicaoCotas)result.ObjectResult };

                #region Coloca o evento na fila pra registro de auditoria
                var eventoAuditoria = new AlteracaoDadosEvent
                {
                    Auditoria = new AuditoriaDto
                    {
                        Agregado = evento.DistribuicaoCotas.GetType().FullName,
                        AgregadoId = evento.DistribuicaoCotas.Id,
                        Operacao = "Encerramento de Período",
                        Conteudo = JsonConvert.SerializeObject(evento.DistribuicaoCotas, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
                    }
                };
                serviceBus.PublishEvent(eventoAuditoria);
                #endregion

                result = bus.PublishEvent(evento);

                if (result.StatusCode == StatusCodeEnum.OK)
                    return new OperationResult(evento.DistribuicaoCotas);
            }

            return result;
        }

        public OperationResult Handle(CalcularRendimentoParcialPeriodoCommand command)
        {
            var percentualRendimento = servicoPeriodo.ObterRendimentoParcial(command.DataReferencia);
            var evento = new IntegracaoRendimentoParcialPeriodoEvent
            {
                DataReferencia = new DateTime(command.DataReferencia.Year, command.DataReferencia.Month, 1),
                DataAtualizacao = DateTime.Today,
                PercentualRendimento = percentualRendimento
            };

            serviceBus.PublishEvent(evento);
            bus.PublishEvent(evento);

            return new OperationResult(percentualRendimento);
        }
        #endregion
    }
}
