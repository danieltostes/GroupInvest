using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos
{
    public interface IServicoPeriodo
    {
        /// <summary>
        /// Inclui um período
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <returns>Resultado da operação</returns>
        OperationResult IncluirPeriodo(Periodo periodo);

        /// <summary>
        /// Altera um período
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <returns>Resultado da operação</returns>
        OperationResult AlterarPeriodo(Periodo periodo);

        /// <summary>
        /// Exclui um período
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <returns>Resultado da operação</returns>
        OperationResult ExcluirPeriodo(Periodo periodo);

        /// <summary>
        /// Encerra um período
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <returns>Resultado da operação</returns>
        OperationResult EncerrarPeriodo(Periodo periodo);

        /// <summary>
        /// Obtém o rendimento parcial do fundo de investimento
        /// </summary>
        /// <param name="dataReferencia">Data de referência</param>
        /// <returns>Percentual de rendimento parcial</returns>
        decimal ObterRendimentoParcial(DateTime dataReferencia);

        /// <summary>
        /// Obtém um período pelo identificador
        /// </summary>
        /// <param name="id">Identificador do período</param>
        /// <returns>Período</returns>
        Periodo ObterPeriodoPorId(int id);

        /// <summary>
        /// Obtém o período ativo
        /// </summary>
        /// <returns>Período</returns>
        Periodo ObterPeriodoAtivo();
    }
}
