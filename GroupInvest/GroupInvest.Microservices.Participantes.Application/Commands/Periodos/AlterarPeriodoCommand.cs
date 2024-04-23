using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Commands.Periodos
{
    public class AlterarPeriodoCommand : PeriodoCommand
    {
        public AlterarPeriodoCommand(int periodoId, int anoReferencia, DateTime? dataInicio, DateTime? dataTermino, bool ativo)
        {
            this.PeriodoId = periodoId;
            this.AnoReferencia = anoReferencia;
            this.DataInicio = dataInicio;
            this.DataTermino = dataTermino;
            this.Ativo = ativo;
        }
    }
}
