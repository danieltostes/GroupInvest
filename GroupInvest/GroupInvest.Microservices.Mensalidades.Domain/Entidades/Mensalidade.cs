using GroupInvest.Common.Domain.Entidades;
using System;

namespace GroupInvest.Microservices.Mensalidades.Domain.Entidades
{
    public class Mensalidade : Entidade<int>
    {
        public Adesao Adesao { get; set; }
        public DateTime? DataReferencia { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal ValorBase { get; set; }
        public decimal ValorDevido { get; set; }
        public decimal ValorPago { get; set; }
        public decimal? PercentualJuros { get; set; }
    }
}
