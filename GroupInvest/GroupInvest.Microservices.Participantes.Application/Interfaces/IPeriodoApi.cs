using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Interfaces
{
    /// <summary>
    /// Interface para a API de período
    /// </summary>
    public interface IPeriodoApi
    {
        /// <summary>
        /// Inclui um novo período
        /// </summary>
        /// <param name="dto">Período</param>
        /// <returns>Resultado da operação</returns>
        OperationResult IncluirPeriodo(PeriodoDto dto);

        /// <summary>
        /// Altera um período existente
        /// </summary>
        /// <param name="dto">Período</param>
        /// <returns>Resultado da operação</returns>
        OperationResult AlterarPeriodo(PeriodoDto dto);

        /// <summary>
        /// Encerra um período ativo
        /// </summary>
        /// <param name="AnoReferencia">Ano de referência do período a ser encerrado</param>
        /// <returns>Resultado da operação</returns>
        OperationResult EncerrarPeriodo(int AnoReferencia);

        /// <summary>
        /// Obtém o período ativo
        /// </summary>
        /// <returns>Período</returns>
        PeriodoDto ObterPeriodoAtivo();

        /// <summary>
        /// Obtém um período pelo ano de referência
        /// </summary>
        /// <param name="anoReferencia">Ano de referência</param>
        /// <returns>Período</returns>
        PeriodoDto ObterPeriodoPorAnoReferencia(int anoReferencia);
    }
}
