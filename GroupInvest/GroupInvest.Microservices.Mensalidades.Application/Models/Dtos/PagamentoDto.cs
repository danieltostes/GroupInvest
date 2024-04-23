using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Dtos
{
    public class PagamentoDto
    {
        public DateTime DataPagamento { get; set; }
        public List<int> Mensalidades { get; set; }
        public List<PagamentoEmprestimoDto> PagamentosEmprestimo { get; set; }

        public PagamentoDto()
        {
            Mensalidades = new List<int>();
            PagamentosEmprestimo = new List<PagamentoEmprestimoDto>();
        }
    }

    public class PagamentoEmprestimoDto
    {
        public int PrevisaoPagamentoId { get; set; }
        public decimal ValorPagamento { get; set; }
    }
}
