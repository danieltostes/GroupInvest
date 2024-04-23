using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para o repositório de participantes
    /// </summary>
    public interface IRepositorioParticipante : IRepositorio<int, Participante>
    {
        /// <summary>
        /// Obtém um participante através do email
        /// </summary>
        /// <param name="email">Email do participante</param>
        /// <returns>Participante</returns>
        Participante ObterParticipantePorEmail(string email);

        /// <summary>
        /// Obtém um usuário pelo identificador do IAm
        /// </summary>
        /// <param name="userId">Identificador do usuário no IAm</param>
        /// <returns>Participante</returns>
        Participante ObterParticipantePorUsuarioAplicativo(string userId);

        /// <summary>
        /// Lista todos os participantes
        /// </summary>
        /// <returns>Lista de participantes</returns>
        IReadOnlyCollection<Participante> ListarTodos();
    }
}
