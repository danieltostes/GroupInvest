using GroupInvest.App.Domain.Entidades;
using GroupInvest.Common.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Domain.Interfaces.Repositorios
{
    public interface IRepositorioDashboard : IHttpRepositorio<Dashboard>
    {
        /// <summary>
        /// Token para as reuisições http
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
