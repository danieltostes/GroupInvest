using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos
{
    /// <summary>
    /// Interface para os serviços de participantes
    /// </summary>
    public interface IServicoParticipante
    {
        /// <summary>
        /// Inclui um participante
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <returns>Resultado da operação</returns>
        OperationResult IncluirParticipante(Participante participante);

        /// <summary>
        /// Exclui um participante
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <returns>Excluir participante</returns>
        OperationResult ExcluirParticipante(Participante participante);

        /// <summary>
        /// Obtém um participante pelo identificador
        /// </summary>
        /// <param name="id">Identificador do participante</param>
        /// <returns>Participante</returns>
        Participante ObterParticipantePorId(int id);
    }
}
