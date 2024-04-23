using GroupInvest.Common.Application.Commands;
using System;

namespace GroupInvest.Microservices.Mensalidades.Application.Commands.Periodos
{
    public abstract class PeriodoCommand : Command
    {
        public int PeriodoId { get; set; }
        public int AnoReferencia { get; set; }
        public DateTime? DataInicioPeriodo { get; set; }
        public DateTime? DataTerminoPeriodo { get; set; }
        public bool Ativo { get; set; }
    }
}
