using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Entidades
{
    public class PagamentoMensalidade
    {
        public DateTime DataPagamento { get; set; }
        public decimal PercentualJuros { get; set; }
        public decimal ValorJuros { get; set; }
        public decimal ValorPago { get; set; }
    }
}
