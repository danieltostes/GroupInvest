using GroupInvest.App.Domain.Entidades;
using GroupInvest.App.Domain.Interfaces.Repositorios;
using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Infra.DataAccess.Repositorios
{
    public class RepositorioMensalidade : HttpRepositorio<Mensalidade>, IRepositorioMensalidade
    {
        #region Construtor
        public RepositorioMensalidade(string urlBase) : base(urlBase)
        {
        }
        #endregion

        #region IRepositorioMensalidade
        public IReadOnlyCollection<Mensalidade> ListarMensalidadesParticipante(int participanteId)
        {
            var response = Get($"mensalidades/participante/{participanteId}");
            var content = response.Content.ReadAsStringAsync().Result;
            var mensalidades = JsonConvert.DeserializeObject<IReadOnlyCollection<Mensalidade>>(content);

            return mensalidades;
        }
        #endregion
    }
}
