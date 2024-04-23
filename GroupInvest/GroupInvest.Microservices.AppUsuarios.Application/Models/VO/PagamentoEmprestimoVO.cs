using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Models.VO
{
    public class PagamentoEmprestimoVO
    {
        public int EmprestimoId { get; set; }
        public decimal PercentualJuros { get; set; }
        public decimal SaldoAtualizado { get; set; }
        public DateTime? DataProximoPagamento { get; set; }
    }
}
