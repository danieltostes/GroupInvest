using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Models.Dtos
{
    public class EmprestimoDto
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorEmprestimo { get; set; }
        public decimal SaldoEmprestimo { get; set; }
        public string Situacao { get; set; }

        public List<PagamentoEmprestimoDto> Pagamentos { get; }

        public EmprestimoDto()
        {
            Pagamentos = new List<PagamentoEmprestimoDto>();
        }
    }
}
