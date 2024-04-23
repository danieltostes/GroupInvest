using GroupInvest.Common.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Entidades
{
    public class PrevisaoPagamentoEmprestimo : Entidade<int>
    {
        public Emprestimo Emprestimo { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorBase { get; set; }
        public decimal ValorDevido { get; set; }
        public decimal PercentualJuros { get; set; }
        public bool Consolidada { get; set; }
        public bool Realizada { get; set; }
    }
}
