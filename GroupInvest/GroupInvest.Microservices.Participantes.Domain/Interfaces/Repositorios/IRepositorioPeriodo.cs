using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para o repositório de período
    /// </summary>
    public interface IRepositorioPeriodo : IRepositorio<int, Periodo>
    {
        /// <summary>
        /// Obtém o período ativo
        /// </summary>
        /// <returns>Período</returns>
        Periodo ObterPeriodoAtivo();

        /// <summary>
        /// Obtém um período em função do ano de referência
        /// </summary>
        /// <param name="anoReferencia">Ano de referência</param>
        /// <returns>Período</returns>
        Periodo ObterPeriodoPorAnoReferencia(int anoReferencia);

        /// <summary>
        /// Lista todos os períodos
        /// </summary>
        /// <returns>Lista de períodos</returns>
        IReadOnlyCollection<Periodo> ListarTodos();
    }
}
