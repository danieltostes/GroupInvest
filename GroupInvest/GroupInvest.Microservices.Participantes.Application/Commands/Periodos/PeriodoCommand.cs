using GroupInvest.Common.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Commands.Periodos
{
    public abstract class PeriodoCommand : Command
    {
        public int PeriodoId { get; set; }
        public int AnoReferencia { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public bool Ativo { get; set; }
    }
}
