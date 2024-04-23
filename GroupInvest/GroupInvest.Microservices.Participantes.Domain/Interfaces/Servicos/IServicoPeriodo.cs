using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Domain.Interfaces.Servicos
{
    /// <summary>
    /// Interface para o serviço de período
    /// </summary>
    public interface IServicoPeriodo
    {
        /// <summary>
        /// Inclui um novo período
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <returns>Resultado da operação</returns>
        OperationResult IncluirPeriodo(Periodo periodo);

        /// <summary>
        /// Altera um período existente
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
        /// Encerra um período ativo
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <returns>Resultado da operação</returns>
        OperationResult EncerrarPeriodo(Periodo periodo);
        
        /// <summary>
        /// Obtém o período ativo
        /// </summary>
        /// <returns>Período ativo</returns>
        Periodo ObterPeriodoAtivo();

        /// <summary>
        /// Obtém um período por ano de referência
        /// </summary>
        /// <param name="anoReferencia">Ano de referência</param>
        /// <returns>Período</returns>
        Periodo ObterPeriodoPorAnoReferencia(int anoReferencia);

        /// <summary>
        /// Lista as adesões ativas dentro de um período
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <returns>Lista de adesões ativas</returns>
        IReadOnlyCollection<Adesao> ListarAdesoesAtivasPeriodo(Periodo periodo);
    }
}
