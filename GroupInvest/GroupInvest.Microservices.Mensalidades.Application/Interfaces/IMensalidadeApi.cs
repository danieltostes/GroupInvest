using GroupInvest.Microservices.Mensalidades.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Interfaces
{
    /// <summary>
    /// Interface para a api de mensalidades
    /// </summary>
    public interface IMensalidadeApi
    {
        /// <summary>
        /// Lista as mensalidades do periodo ativo de um participante
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Lista de mensalidades</returns>
        IReadOnlyCollection<MensalidadeDto> ListarMensalidadesParticipante(int participanteId);
    }
}
