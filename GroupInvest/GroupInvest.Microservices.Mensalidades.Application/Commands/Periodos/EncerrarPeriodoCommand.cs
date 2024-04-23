using GroupInvest.Common.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Commands.Periodos
{
    public class EncerrarPeriodoCommand : Command
    {
        public int PeriodoId { get; set; }

        public EncerrarPeriodoCommand(int periodoId)
        {
            this.PeriodoId = periodoId;
        }
    }
}
