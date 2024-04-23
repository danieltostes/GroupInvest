using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Emprestimos;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Emprestimos;
using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.CommandHandlers
{
    public class EmprestimoCommandHandler : CommandHandler, IEmprestimoCommandHandler
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        private IServicoEmprestimo servicoEmprestimo;
        #endregion

        #region Construtor
        public EmprestimoCommandHandler(IMediatorHandler bus, IServicoEmprestimo servicoEmprestimo)
        {
            this.bus = bus;
            this.servicoEmprestimo = servicoEmprestimo;
        }
        #endregion

        #region IEmprestimoCommandHandler
        public OperationResult Handle(RegistrarEmprestimoConcedidoCommand command)
        {
            var emprestimo = new Emprestimo
            {
                EmprestimoId = command.Emprestimo.Id,
                ParticipanteId = command.Emprestimo.ParticipanteId,
                Data = command.Emprestimo.Data,
                //ProximoPagamento = new ProximoPagamentoEmprestimo
                //{
                //    DataVencimento = command.Emprestimo.DataProximoVencimento,
                //    ValorDevido = command.Emprestimo.Saldo,
                //    PagamentoMinimo = command.Emprestimo.Saldo - command.Emprestimo.Valor
                //},
                Valor = command.Emprestimo.Valor,
                Saldo = command.Emprestimo.Saldo
            };

            servicoEmprestimo.IncluirEmprestimo(emprestimo);
            bus.PublishEvent(new EmprestimoConcedidoRegistradoEvent());

            return OperationResult.OK;
        }

        public OperationResult Handle(RegistrarAtualizacaoSaldoEmprestimosCommand command)
        {
            foreach (var item in command.Saldos)
            {
                var emprestimo = servicoEmprestimo.ObterEmprestimoPorId(item.EmprestimoId);
                emprestimo.Saldo = item.Saldo;

                // atualiza a previsão de próximo pagamento
                //if (emprestimo.ProximoPagamento != null)
                //{
                //    var dia = emprestimo.ProximoPagamento.DataVencimento.Day;
                //    var mes = DateTime.Today.Day <= dia ? DateTime.Today.Month : DateTime.Today.AddMonths(1).Month;
                //    var dataPrevisaoPagamento = new DateTime(emprestimo.ProximoPagamento.DataVencimento.Year, mes, dia);

                //    emprestimo.ProximoPagamento.DataVencimento = dataPrevisaoPagamento;
                //    emprestimo.ProximoPagamento.ValorDevido = emprestimo.Saldo;
                //    emprestimo.ProximoPagamento.PagamentoMinimo = 0;
                //}

                servicoEmprestimo.AtualizarSaldoEmprestimo(emprestimo);
            }
            return OperationResult.OK;
        }
        #endregion
    }
}
