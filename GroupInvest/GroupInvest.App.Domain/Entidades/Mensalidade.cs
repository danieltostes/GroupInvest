using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Domain.Entidades
{
    public class Mensalidade
    {
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }

        public decimal ValorDevido { get; set; }
        public decimal? ValorPago { get; set; }
        public decimal? PercentualJuros { get; set; }
    }
}
