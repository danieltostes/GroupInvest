using System;

namespace GroupInvest.Microservices.Mensalidades.Application.Commands.Periodos
{
    public class IncluirPeriodoCommand : PeriodoCommand
    {
        #region Construtor
        public IncluirPeriodoCommand()
        {
            // construtor vazio para o auto mapper
        }

        public IncluirPeriodoCommand(int periodoId, int anoReferencia, DateTime? dataInicioPeriodo, DateTime? dataTerminoPeriodo, bool ativo)
        {
            PeriodoId = periodoId;
            AnoReferencia = anoReferencia;
            DataInicioPeriodo = dataInicioPeriodo;
            DataTerminoPeriodo = dataTerminoPeriodo;
            Ativo = ativo;
        }
        #endregion
    }
}
