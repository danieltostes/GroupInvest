using GroupInvest.Microservices.AppUsuarios.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Interfaces.APIs
{
    /// <summary>
    /// Interface para a API de Mensalidades
    /// </summary>
    public interface IMensalidadeApi
    {
        /// <summary>
        /// Lista as mensalidades de um participante
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Lista de mensalidades</returns>
        IReadOnlyCollection<MensalidadeDto> ListarMensalidadesParticipante(int participanteId);
    }
}
