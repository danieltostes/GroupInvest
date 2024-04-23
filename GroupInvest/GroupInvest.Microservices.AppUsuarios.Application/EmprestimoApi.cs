using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.APIs;
using GroupInvest.Microservices.AppUsuarios.Application.Models.Dtos;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application
{
    public class EmprestimoApi : IEmprestimoApi
    {
        #region Injeção de dependência
        private readonly IServicoEmprestimo servicoEmprestimo;
        #endregion

        #region Construtor
        public EmprestimoApi(IServicoEmprestimo servicoEmprestimo)
        {
            this.servicoEmprestimo = servicoEmprestimo;
        }
        #endregion

        #region IEmprestimoApi
        public IReadOnlyCollection<EmprestimoDto> ListarEmprestimosParticipante(int participanteId)
        {
            var emprestimos = servicoEmprestimo.ListarEmprestimosParticipante(participanteId);
            var lista = new List<EmprestimoDto>();

            foreach (var emprestimo in emprestimos)
            {
                var dto = new EmprestimoDto
                {
                    Id = emprestimo.EmprestimoId,
                    Data = emprestimo.Data,
                    ValorEmprestimo = emprestimo.Valor,
                    SaldoEmprestimo = emprestimo.Saldo,
                    Situacao = emprestimo.Situacao
                };

                if (emprestimo.Pagamentos.Count > 0)
                {
                    foreach (var pagamento in emprestimo.Pagamentos.OrderBy(p => p.DataPagamento))
                    {
                        var pagamentoDto = new PagamentoEmprestimoDto();
                        
                        pagamentoDto.DataPagamento = pagamento.DataPagamento;
                        pagamentoDto.PercentualJuros = pagamento.PercentualJuros;
                        pagamentoDto.SaldoAnterior = pagamento.SaldoAnterior;
                        pagamentoDto.SaldoAtualizado = pagamento.SaldoAtualizado;
                        pagamentoDto.ValorDevido = pagamento.ValorDevido;
                        pagamentoDto.ValorJuros = pagamento.ValorJuros;
                        pagamentoDto.ValorPago = pagamento.ValorPago;

                        dto.Pagamentos.Add(pagamentoDto);
                    }
                }

                lista.Add(dto);
            }

            return lista;
        }

        public EmprestimoDto ObterEmprestimo(int emprestimoId)
        {
            var emprestimo = servicoEmprestimo.ObterEmprestimoPorId(emprestimoId);
            if (emprestimo == null)
                return null;

            var dto = new EmprestimoDto
            {
                Id = emprestimo.EmprestimoId,
                Data = emprestimo.Data,
                ValorEmprestimo = emprestimo.Valor,
                SaldoEmprestimo = emprestimo.Saldo,
                Situacao = emprestimo.Situacao
            };

            if (emprestimo.Pagamentos.Count > 0)
            {
                foreach (var pagamento in emprestimo.Pagamentos)
                {
                    var pagamentoDto = new PagamentoEmprestimoDto();

                    pagamentoDto.DataPagamento = pagamento.DataPagamento;
                    pagamentoDto.PercentualJuros = pagamento.PercentualJuros;
                    pagamentoDto.SaldoAnterior = pagamento.SaldoAnterior;
                    pagamentoDto.SaldoAtualizado = pagamento.SaldoAtualizado;
                    pagamentoDto.ValorDevido = pagamento.ValorDevido;
                    pagamentoDto.ValorJuros = pagamento.ValorJuros;
                    pagamentoDto.ValorPago = pagamento.ValorPago;

                    dto.Pagamentos.Add(pagamentoDto);
                }
            }

            return dto;
        }
        #endregion
    }
}
