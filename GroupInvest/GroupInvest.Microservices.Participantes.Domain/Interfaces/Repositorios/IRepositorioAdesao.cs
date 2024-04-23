using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para o repositório de Adesão
    /// </summary>
    public interface IRepositorioAdesao : IRepositorio<int, Adesao>
    {
        /// <summary>
        /// Obtém uma adesão
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <param name="periodo">Período</param>
        /// <returns>Adesão</returns>
        Adesao ObterAdesao(Participante participante, Periodo periodo);

        /// <summary>
        /// Lista todas as adesões
        /// </summary>
        /// <returns>Lista de adesões</returns>
        IReadOnlyCollection<Adesao> ListarTodos();

        /// <summary>
        /// Lista as adesões ativas de um período
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <returns>Lista de adesões ativas</returns>
        IReadOnlyCollection<Adesao> ListarAdesoesAtivasPeriodo(Periodo periodo);
    }
}
