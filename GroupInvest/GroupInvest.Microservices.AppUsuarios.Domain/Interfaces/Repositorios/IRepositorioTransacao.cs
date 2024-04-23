using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios
{
    public interface IRepositorioTransacao : IMongoRepositorio<Transacao>
    {
        /// <summary>
        /// Lista as últimas transações de um participante
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Lista de últimas transações</returns>
        IReadOnlyCollection<Transacao> ListarUltimasTransacoesParticipante(int participanteId);
    }
}
