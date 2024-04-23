using GroupInvest.Common.Domain.Entidades;
using System;
using System.Collections.Generic;

namespace GroupInvest.Microservices.Mensalidades.Domain.Entidades
{
    public class Emprestimo : Entidade<int>
    {
        public Adesao Adesao { get; set; }
        public DateTime Data { get; set; }
        public DateTime? DataProximoVencimento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public bool Quitado { get; set; }
        public ICollection<PagamentoParcialEmprestimo> PagamentosParciais { get; set; }
    }
}
