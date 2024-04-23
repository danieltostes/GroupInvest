using GroupInvest.Common.Application.Commands;
using System;

namespace GroupInvest.Microservices.Mensalidades.Application.Commands.Mensalidades
{
    public class MensalidadeCommand : Command
    {
        public int Id { get; set; }
        public int AdesaoId { get; set; }
        public DateTime? DataReferencia { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal ValorDevido { get; set; }
        public decimal ValorPago { get; set; }
    }
}
