using GroupInvest.Common.Domain.Entidades;
using System;

namespace GroupInvest.Microservices.Mensalidades.Domain.Entidades
{
    public class PagamentoParcialEmprestimo : Entidade<int>
    {
        public Emprestimo Emprestimo { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPagamento { get; set; }
    }
}
