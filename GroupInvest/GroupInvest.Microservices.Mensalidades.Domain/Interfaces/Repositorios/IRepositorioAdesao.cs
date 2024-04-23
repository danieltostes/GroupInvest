using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para os repositórios de Adesão
    /// </summary>
    public interface IRepositorioAdesao : IRepositorio<int, Adesao>
    {
        /// <summary>
        /// Obtém uma adesão
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <param name="periodoId">Identificador do período</param>
        /// <returns>Adesão</returns>
        Adesao ObterAdesao(int participanteId, int periodoId);

        /// <summary>
        /// Lista todas as adesões de um período
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <returns>Lista de adesões</returns>
        IReadOnlyCollection<Adesao> ListarAdesoesPeriodo(Periodo periodo);

        /// <summary>
        /// Obtém o número total de cotas de um período
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <returns>Número total de cotas</returns>
        int ObterTotalCotasPeriodo(Periodo periodo);

    }
}
