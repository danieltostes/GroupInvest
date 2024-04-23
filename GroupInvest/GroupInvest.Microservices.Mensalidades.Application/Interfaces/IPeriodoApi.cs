using GroupInvest.Common.Domain.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Interfaces
{
    /// <summary>
    /// Interface para a api de períodos
    /// </summary>
    public interface IPeriodoApi
    {
        OperationResult EncerrarPeriodo(int id);
        OperationResult CalcularRendimentoParcialPeriodo(DateTime dataReferencia);
    }
}
