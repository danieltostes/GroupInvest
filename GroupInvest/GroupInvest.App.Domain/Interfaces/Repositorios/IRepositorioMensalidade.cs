using GroupInvest.App.Domain.Entidades;
using GroupInvest.Common.Domain.Interfaces;
using System.Collections.Generic;

namespace GroupInvest.App.Domain.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para repositório de mensalidades
    /// </summary>
    public interface IRepositorioMensalidade : IHttpRepositorio<Mensalidade>
    {
        /// <summary>
        /// Token para requisições http
        /// </summary>
        string Token { get; set; }

        /// <summary>
        /// Lista as mensalidades de um participante
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Lista de mensalidades</returns>
        IReadOnlyCollection<Mensalidade> ListarMensalidadesParticipante(int participanteId);
    }
}
