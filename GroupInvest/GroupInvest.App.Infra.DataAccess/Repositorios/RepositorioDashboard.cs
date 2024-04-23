using GroupInvest.App.Domain.Entidades;
using GroupInvest.App.Domain.Interfaces.Repositorios;
using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Infra.DataAccess.Repositorios
{
    public class RepositorioDashboard : HttpRepositorio<Dashboard>, IRepositorioDashboard
    {
        #region Construtor
        public RepositorioDashboard(string urlBase) : base(urlBase)
        {
        }
        #endregion

        #region IRepositorioDashboard
        public Dashboard ObterDashboardParticipante(int participanteId)
        {
            var response = Get($"dashboard/{participanteId}");
            var content = response.Content.ReadAsStringAsync().Result;
            var dashboard = JsonConvert.DeserializeObject<Dashboard>(content);

            return dashboard;
        }
        #endregion
    }
}
