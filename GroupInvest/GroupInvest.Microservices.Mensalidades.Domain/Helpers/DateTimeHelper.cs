using System;
using System.Linq;

namespace GroupInvest.Microservices.Mensalidades.Domain.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime ObterDataDiaUtil(DateTime dataReferencia)
        {
            var data = dataReferencia;

            //Trata a data de referência para não considerar sábado e domingo - posteriormente deve considerar feriados
            var diasFimDeSemana = new int[] { 0, 6 };
            bool dataInvalida = diasFimDeSemana.Contains((int)data.DayOfWeek);
            while (dataInvalida)
            {
                data = data.AddDays(1);
                dataInvalida = diasFimDeSemana.Contains((int)data.DayOfWeek);
            }

            return data;
        }
    }
}
