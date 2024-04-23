using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Pagamentos;
using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.CommandHandlers
{
    public class PagamentoCommandHandler : CommandHandler, IPagamentoCommandHandler
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        private readonly IServicoMensalidade servicoMensalidade;
        private readonly IServicoEmprestimo servicoEmprestimo;
        #endregion

        #region Construtor
        public PagamentoCommandHandler(IMediatorHandler bus, IServicoMensalidade servicoMensalidade, IServicoEmprestimo servicoEmprestimo)
        {
            this.bus = bus;
            this.servicoMensalidade = servicoMensalidade;
            this.servicoEmprestimo = servicoEmprestimo;
        }
        #endregion

        #region IPagamentoCommandHandler
        public OperationResult Handle(RegistrarPagamentoCommand command)
        {
            var mensalidades = command.ItensPagamento.Where(it => it.Mensalidade != null);
            var emprestimos = command.ItensPagamento.Where(it => it.Emprestimo != null);

            // registra os pagamentos de mensalidades
            foreach (var vo in mensalidades)
            {
                var mensalidade = servicoMensalidade.ObterMensalidadePorId(vo.Mensalidade.Id);
                if (mensalidade != null)
                {
                    var pagamento = new PagamentoMensalidade
                    {
                        DataPagamento = command.DataPagamento,
                        ValorPago = vo.Valor,
                        PercentualJuros = vo.Mensalidade.PercentualJuros.GetValueOrDefault(),
                        ValorJuros = vo.Valor - vo.Mensalidade.ValorBase
                    };
                    servicoMensalidade.IncluirPagamento(mensalidade, pagamento);
                }
            }

            // registra os pagamentos de empréstimos
            foreach (var vo in emprestimos)
            {
                var emprestimo = servicoEmprestimo.ObterEmprestimoPorId(vo.Emprestimo.EmprestimoId);

                var pagamentoEmprestimo = new PagamentoEmprestimo
                {
                    DataPagamento = command.DataPagamento,
                    ValorPago = vo.Valor,
                    SaldoAnterior = emprestimo.Saldo,
                    ValorDevido = emprestimo.Saldo,
                    SaldoAtualizado = vo.Emprestimo.SaldoAtualizado,
                    PercentualJuros = vo.Emprestimo.PercentualJuros,
                    ValorJuros = 0
                };

                servicoEmprestimo.AdicionarPagamento(emprestimo, pagamentoEmprestimo);

                emprestimo.Saldo = vo.Emprestimo.SaldoAtualizado;
                servicoEmprestimo.AtualizarSaldoEmprestimo(emprestimo);
            }

            return OperationResult.OK;
        }
        #endregion
    }
}
