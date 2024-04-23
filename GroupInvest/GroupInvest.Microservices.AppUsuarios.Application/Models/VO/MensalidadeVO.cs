using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Models.VO
{
    public class MensalidadeVO
    {
        public int Id { get; set; }
        public DateTime? DataReferencia { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal ValorBase { get; set; }
        public decimal ValorDevido { get; set; }
        public decimal ValorPago { get; set; }
        public decimal? PercentualJuros { get; set; }
    }
}
