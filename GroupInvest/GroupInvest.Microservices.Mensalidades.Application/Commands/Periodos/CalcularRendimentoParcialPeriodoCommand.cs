using GroupInvest.Common.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Commands.Periodos
{
    public class CalcularRendimentoParcialPeriodoCommand : Command
    {
        public DateTime DataReferencia { get; }

        public CalcularRendimentoParcialPeriodoCommand(DateTime dataReferencia)
        {
            DataReferencia = dataReferencia;
        }
    }
}
