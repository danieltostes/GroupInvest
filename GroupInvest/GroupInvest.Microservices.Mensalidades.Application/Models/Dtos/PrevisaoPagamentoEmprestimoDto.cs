using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Dtos
{
    public class PrevisaoPagamentoEmprestimoDto
    {
        public int Id { get; set; }
        public int EmprestimoId { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorBase { get; set; }
        public decimal ValorDevido { get; set; }
        public decimal PercentualJuros { get; set; }
        public bool Consolidada { get; set; }
        public bool Realizada { get; set; }
    }
}
