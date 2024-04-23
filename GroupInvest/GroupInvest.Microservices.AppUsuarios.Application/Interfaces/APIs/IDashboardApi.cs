using GroupInvest.Microservices.AppUsuarios.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Interfaces.APIs
{
    /// <summary>
    /// Interface para a API de Dashboard
    /// </summary>
    public interface IDashboardApi
    {
        /// <summary>
        /// Obtém o dashboard de um participante
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Dashboard</returns>
        DashboardDto ObterDashboardParticipante(int participanteId);
    }
}
