using AutoMapper;
using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Pagamentos;
using GroupInvest.Microservices.Mensalidades.Application.Events;
using GroupInvest.Microservices.Mensalidades.Application.Events.Pagamentos;
using GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces;
using GroupInvest.Microservices.Mensalidades.Domain.Classes;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using GroupInvest.Microservices.Mensalidades.Application.Events.Mensalidades;
using GroupInvest.Microservices.Mensalidades.Application.Events.Emprestimos;
using GroupInvest.Microservices.Mensalidades.Application.Models.VO;
using GroupInvest.Microservices.Mensalidades.Application.Enumerados;

namespace GroupInvest.Microservices.Mensalidades.Application.CommandHandlers
{
    public class PagamentoCommandHandler : CommandHandler, IPagamentoCommandHandler
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        private readonly IMediatorHandlerQueue serviceBus;
        private readonly IMapper mapper;
        private readonly IServicoPagamento servicoPagamento;
        private readonly IServicoMensalidade servicoMensalidade;
        private readonly IServicoEmprestimo servicoEmprestimo;
        #endregion

        #region Construtor
        public PagamentoCommandHandler(
            IMediatorHandler bus,
            IMediatorHandlerQueue serviceBus,
            IMapper mapper,
            IServicoPagamento servicoPagamento,
            IServicoMensalidade servicoMensalidade,
            IServicoEmprestimo servicoEmprestimo)
        {
            this.bus = bus;
            this.serviceBus = serviceBus;
            this.mapper = mapper;
            this.servicoPagamento = servicoPagamento;
            this.servicoMensalidade = servicoMensalidade;
            this.servicoEmprestimo = servicoEmprestimo;
        }
        #endregion

        #region IPagamentoCommandHandler
        public OperationResult Handle(RealizarPagamentoCommand command)
        {
            return RealizarPagamento(command, false);
        }

        public OperationResult Handle(RealizarPagamentoRetroativoCommand command)
        {
            return RealizarPagamento(command, true);
        }
        #endregion

        #region Métodos Privados
        private OperationResult RealizarPagamento(RealizarPagamentoBaseCommand command, bool retroativo)
        {
            Fatura fatura = new Fatura(command.PagamentoDto.DataPagamento);

            #region Adiciona as mensalidades à fatura de pagamento
            if (command.PagamentoDto.Mensalidades.Count > 0)
            {
                foreach (var mensalidadeId in command.PagamentoDto.Mensalidades)
                {
                    var mensalidade = servicoMensalidade.ObterMensalidadePorId(mensalidadeId);
                    if (mensalidade == null)
                        return new OperationResult(StatusCodeEnum.Error, $"Mensalidade Id: {mensalidadeId} não foi encontrada.");

                    fatura.AdicionarMensalidade(mensalidade);
                }
            }
            #endregion

            #region Adiciona as previsões de pagamento de empréstimo à fatura
            if (command.PagamentoDto.PagamentosEmprestimo.Count > 0)
            {
                foreach (var pagamentoEmprestimo in command.PagamentoDto.PagamentosEmprestimo)
                {
                    var previsaoPagamento = servicoEmprestimo.ObterPrevisaoPagamentoEmprestimoPorId(pagamentoEmprestimo.PrevisaoPagamentoId);
                    if (previsaoPagamento == null)
                        return new OperationResult(StatusCodeEnum.Error, $"Previsão de pagamento Id: {pagamentoEmprestimo.PrevisaoPagamentoId} não foi encontrada");

                    // Tratamento para exceções de valor de pagamento inválido
                    try { fatura.AdicionarPrevisaoPagamentoEmprestimo(previsaoPagamento, pagamentoEmprestimo.ValorPagamento); }
                    catch (Exception ex) { return new OperationResult(StatusCodeEnum.Error, ex.Message); }
                }
            }
            #endregion

            var result = retroativo ? servicoPagamento.RealizarPagamentoRetroativo(fatura) : servicoPagamento.RealizarPagamento(fatura);

            if (result.StatusCode == StatusCodeEnum.OK)
            {
                var pagamento = (Pagamento)result.ObjectResult;

                var dynamicObjectResult = new
                {
                    PagamentoId = pagamento.Id,
                    pagamento.DataPagamento,
                    pagamento.ValorTotalPagamento,
                    NumeroMensalidadesPagas = pagamento.ItensPagamento.Where(it => it.Mensalidade != null).Count(),
                    NumeroEmprestimosPagos = pagamento.ItensPagamento.Where(it => it.PrevisaoPagamentoEmprestimo != null)
                                .GroupBy(it => it.PrevisaoPagamentoEmprestimo.Emprestimo).Count()
                };

                #region coloca evento de auditoria na fila do serviceBus
                var alteracaoDadosEvent = new AlteracaoDadosEvent
                {
                    Auditoria = new Models.Dtos.AuditoriaDto
                    {
                        Agregado = pagamento.GetType().FullName,
                        AgregadoId = pagamento.Id,
                        Operacao = "Realização de pagamento",
                        Conteudo = JsonConvert.SerializeObject(dynamicObjectResult),
                        Timestamp = DateTime.Now
                    }
                };

                serviceBus.PublishEvent(alteracaoDadosEvent);
                #endregion

                #region Evento de integração de pagamento
                var eventoIntegracao = new IntegracaoPagamentoRealizadoEvent
                {
                    Id = pagamento.Id,
                    DataPagamento = pagamento.DataPagamento,
                    ValorTotalPagamento = pagamento.ValorTotalPagamento
                };

                // adiciona as mensalidades pagas
                foreach (var item in pagamento.ItensPagamento.Where(it => it.Mensalidade != null))
                {
                    var itemVO = new ItemPagamentoVO
                    {
                        Id = item.Id,
                        Valor = item.Valor
                    };
                    itemVO.Mensalidade = mapper.Map<Mensalidade, MensalidadeVO>(item.Mensalidade);
                    eventoIntegracao.ItensPagamento.Add(itemVO);
                }

                // adiciona informação agrupada de empréstimos pagos
                var itensEmprestimo = pagamento.ItensPagamento
                    .Where(it => it.PrevisaoPagamentoEmprestimo != null)
                    .GroupBy(it => it.PrevisaoPagamentoEmprestimo.Emprestimo);

                foreach (var item in itensEmprestimo)
                {
                    var itemVO = new ItemPagamentoVO
                    {
                        Id = item.Key.Id,
                        Valor = item.Sum(it => it.Valor)
                    };

                    var percentualJuros = item.Max(it => it.PrevisaoPagamentoEmprestimo.PercentualJuros);
                    var pagamentoEmprestimoVO = new PagamentoEmprestimoVO
                    {
                        EmprestimoId = item.Key.Id,
                        SaldoAtualizado = item.Key.Saldo,
                        DataProximoPagamento = item.Key.DataProximoVencimento,
                        PercentualJuros = percentualJuros
                    };

                    itemVO.Emprestimo = pagamentoEmprestimoVO;
                    eventoIntegracao.ItensPagamento.Add(itemVO);
                }

                // publica na fila de integração de pagamentos
                serviceBus.PublishEvent(eventoIntegracao);
                #endregion

                #region Evento de integração de transação
                var itemPagamento = pagamento.ItensPagamento.First();
                var participanteId = itemPagamento.Mensalidade != null ?
                    itemPagamento.Mensalidade.Adesao.Participante.Id :
                    itemPagamento.PrevisaoPagamentoEmprestimo.Emprestimo.Adesao.Participante.Id;

                var eventoTransacao = new TransacaoRealizadaEvent
                {
                    ParticipanteId = participanteId,
                    CodigoTransacao = (int)TipoTransacaoEnum.Pagamento,
                    DataTransacao = pagamento.DataPagamento,
                    Descricao = "Pagamento",
                    ValorTransacao = pagamento.ValorTotalPagamento
                };
                serviceBus.PublishEvent(eventoTransacao);
                #endregion

                if (retroativo)
                {
                    var evento = new PagamentoRetroativoRealizadoEvent(pagamento);
                    result = bus.PublishEvent(evento);
                }
                else
                {
                    var evento = new PagamentoRealizadoEvent(pagamento);
                    result = bus.PublishEvent(evento);
                }

                if (result.StatusCode == StatusCodeEnum.OK)
                    result = new OperationResult(dynamicObjectResult);
            }

            return result;
        }
        #endregion
    }
}
