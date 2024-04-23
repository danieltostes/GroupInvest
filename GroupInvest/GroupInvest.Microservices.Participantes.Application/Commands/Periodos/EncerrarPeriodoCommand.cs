using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Commands.Periodos
{
    public class EncerrarPeriodoCommand : PeriodoCommand
    {
        public EncerrarPeriodoCommand(int anoReferencia)
        {
            this.AnoReferencia = anoReferencia;
        }
    }
}
