using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos
{
    public interface IServicoTransacao
    {
        /// <summary>
        /// Inclui uma transação no repositório
        /// </summary>
        /// <param name="transacao">Transação</param>
        /// <returns>Resultado da operação</returns>
        OperationResult IncluirTransacao(Transacao transacao);

        /// <summary>
        /// Lista as últimas transações de um participante
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Lista de últimas transações</returns>
        IReadOnlyCollection<Transacao> ListarUltimasTransacoesParticipante(int participanteId);
    }
}
