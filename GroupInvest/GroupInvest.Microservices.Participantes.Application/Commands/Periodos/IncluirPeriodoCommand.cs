using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Commands.Periodos
{
    public class IncluirPeriodoCommand : PeriodoCommand
    {
        public IncluirPeriodoCommand(int anoReferencia, DateTime? dataInicio, DateTime? dataTermino, bool ativo)
        {
            this.AnoReferencia = anoReferencia;
            this.DataInicio = dataInicio;
            this.DataTermino = dataTermino;
            this.Ativo = ativo;
        }
    }
}
