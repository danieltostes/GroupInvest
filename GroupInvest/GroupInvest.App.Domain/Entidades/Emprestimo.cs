using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Domain.Entidades
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorEmprestimo { get; set; }
        public decimal SaldoEmprestimo { get; set; }
        public string Situacao { get; set; }

        public List<PagamentoEmprestimo> Pagamentos { get; set; }

        public Emprestimo()
        {
            Pagamentos = new List<PagamentoEmprestimo>();
        }
    }
}
