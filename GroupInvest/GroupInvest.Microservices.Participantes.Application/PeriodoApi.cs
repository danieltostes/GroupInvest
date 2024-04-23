using AutoMapper;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Application.Commands.Periodos;
using GroupInvest.Microservices.Participantes.Application.Interfaces;
using GroupInvest.Microservices.Participantes.Application.Models.Dtos;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application
{
    public class PeriodoApi : IPeriodoApi
    {
        private readonly IMediatorHandler bus;
        private readonly IServicoPeriodo servicoPeriodo;
        private readonly IMapper mapper;

        #region Construtor
        public PeriodoApi(IMediatorHandler bus, IServicoPeriodo servicoPeriodo, IMapper mapper)
        {
            this.bus = bus;
            this.servicoPeriodo = servicoPeriodo;
            this.mapper = mapper;
        }
        #endregion

        #region Commands
        public OperationResult IncluirPeriodo(PeriodoDto dto)
        {
            var command = new IncluirPeriodoCommand(dto.AnoReferencia, dto.DataInicio, dto.DataTermino, dto.Ativo);
            return bus.SendCommand(command);
        }

        public OperationResult AlterarPeriodo(PeriodoDto dto)
        {
            var command = new AlterarPeriodoCommand(dto.Id, dto.AnoReferencia, dto.DataInicio, dto.DataTermino, dto.Ativo);
            return bus.SendCommand(command);
        }

        public OperationResult EncerrarPeriodo(int AnoReferencia)
        {
            var command = new EncerrarPeriodoCommand(AnoReferencia);
            return bus.SendCommand(command);
        }
        #endregion

        #region Commands
        public PeriodoDto ObterPeriodoAtivo()
        {
            var periodo = servicoPeriodo.ObterPeriodoAtivo();
            return mapper.Map<Periodo, PeriodoDto>(periodo);
        }

        public PeriodoDto ObterPeriodoPorAnoReferencia(int anoReferencia)
        {
            var periodo = servicoPeriodo.ObterPeriodoPorAnoReferencia(anoReferencia);
            return mapper.Map<Periodo, PeriodoDto>(periodo);
        }
        #endregion
    }
}
