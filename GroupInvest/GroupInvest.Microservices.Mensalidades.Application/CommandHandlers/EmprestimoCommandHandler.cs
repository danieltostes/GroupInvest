using AutoMapper;
using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Emprestimos;
using GroupInvest.Microservices.Mensalidades.Application.Enumerados;
using GroupInvest.Microservices.Mensalidades.Application.Events;
using GroupInvest.Microservices.Mensalidades.Application.Events.Emprestimos;
using GroupInvest.Microservices.Mensalidades.Application.Helpers;
using GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces;
using GroupInvest.Microservices.Mensalidades.Application.Models.VO;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;

namespace GroupInvest.Microservices.Mensalidades.Application.CommandHandlers
{
    public class EmprestimoCommandHandler : CommandHandler, IEmprestimoCommandHandler
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        private readonly IMediatorHandlerQueue serviceBus;
        private readonly IMapper mapper;
        private readonly IServicoEmprestimo servicoEmprestimo;
        private readonly IServicoParticipante servicoParticipante;
        #endregion

        #region Construtor
        public EmprestimoCommandHandler(IMediatorHandler bus, IMediatorHandlerQueue serviceBus, IMapper mapper, IServicoEmprestimo servicoEmprestimo, IServicoParticipante servicoParticipante)
        {
            this.bus = bus;
            this.serviceBus = serviceBus;
            this.mapper = mapper;
            this.servicoEmprestimo = servicoEmprestimo;
            this.servicoParticipante = servicoParticipante;
        }
        #endregion

        public OperationResult Handle(ConcederEmprestimoCommand command)
        {
            var participante = servicoParticipante.ObterParticipantePorId(command.ParticipanteId);

            if (participante == null)
                return new OperationResult(StatusCodeEnum.Error, string.Format("Participante não encontrado para a concessão de empréstimo. Id: {0}", command.ParticipanteId));

            var result = servicoEmprestimo.ConcederEmprestimo(participante, command.DataEmprestimo, command.ValorEmprestimo);
            if (result.StatusCode == StatusCodeEnum.OK)
            {
                var emprestimo = (Emprestimo)result.ObjectResult;

                #region Eventos de integração
                var eventSource = EventSourcingHelper.CreateEvent("Concessão de Empréstimo", emprestimo);
                serviceBus.PublishEvent(eventSource);

                var evento = new EmprestimoConcedidoEvent { Emprestimo = mapper.Map<Emprestimo, EmprestimoVO>(emprestimo) };
                serviceBus.PublishEvent(evento);

                var eventoTransacao = new TransacaoRealizadaEvent
                {
                    ParticipanteId = emprestimo.Adesao.Participante.Id,
                    CodigoTransacao = (int)TipoTransacaoEnum.Emprestimo,
                    DataTransacao = emprestimo.Data,
                    Descricao = "Empréstimo",
                    ValorTransacao = emprestimo.Valor
                };
                serviceBus.PublishEvent(eventoTransacao);
                #endregion

                bus.PublishEvent(evento);
            }

            return result;
        }

        public OperationResult Handle(AtualizarPrevisoesPagamentoEmprestimosCommand command)
        {
            var emprestimos = servicoEmprestimo.ListarEmprestimosEmAberto();

            var saldosAnteriores = new Dictionary<int, decimal>();

            if (emprestimos.Count.Equals(0))
                return OperationResult.OK;

            foreach (var emprestimo in emprestimos)
            {
                saldosAnteriores.Add(emprestimo.Id, emprestimo.Saldo);
                var result = servicoEmprestimo.AtualizarPrevisaoPagamentoEmprestimo(emprestimo, command.DataReferencia);
                if (result.StatusCode == StatusCodeEnum.Error)
                    return result;
            }

            var evento = new PrevisoesPagamentoEmprestimosAtualizadasEvent();

            foreach (var emprestimo in emprestimos)
            {
                if (saldosAnteriores[emprestimo.Id] != emprestimo.Saldo)
                    evento.Saldos.Add(new SaldoEmprestimoVO { EmprestimoId = emprestimo.Id, Saldo = emprestimo.Saldo });
            }

            if (evento.Saldos.Count > 0)
            {
                serviceBus.PublishEvent(evento);
                bus.PublishEvent(evento);
            }

            return new OperationResult(evento.Saldos);
        }
    }
}
