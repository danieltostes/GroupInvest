using System;

namespace GroupInvest.Microservices.Mensalidades.Application.Commands.Periodos
{
    public class AlterarPeriodoCommand : PeriodoCommand
    {
        #region Construtor
        public AlterarPeriodoCommand(int periodoId, int anoReferencia, DateTime? dataInicioPeriodo, DateTime? dataTerminoPeriodo)
        {
            PeriodoId = periodoId;
            AnoReferencia = anoReferencia;
            DataInicioPeriodo = dataInicioPeriodo;
            DataTerminoPeriodo = dataTerminoPeriodo;
        }
        #endregion
    }
}
