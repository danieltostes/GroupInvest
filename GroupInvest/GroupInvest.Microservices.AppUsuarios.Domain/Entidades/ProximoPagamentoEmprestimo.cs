using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Entidades
{
    public class ProximoPagamentoEmprestimo
    {
        public DateTime DataVencimento { get; set; }
        public decimal PagamentoMinimo { get; set; }
        public decimal ValorDevido { get; set; }
    }
}
