using GroupInvest.App.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Domain.Interfaces.Servicos
{
    public interface IServicoDashboard
    {
        /// <summary>
        /// Token para as requisições http
        /// </summary>
        string Token { get; set; }

        /// <summary>
        /// Obtém o dashboard de um participante
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Dashboard</returns>
        Dashboard ObterDashboardParticipante(int participanteId);
    }
}
