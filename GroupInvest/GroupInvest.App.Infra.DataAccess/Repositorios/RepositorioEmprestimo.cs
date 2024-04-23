using GroupInvest.App.Domain.Entidades;
using GroupInvest.App.Domain.Interfaces.Repositorios;
using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Infra.DataAccess.Repositorios
{
    public class RepositorioEmprestimo : HttpRepositorio<Emprestimo>, IRepositorioEmprestimo
    {
        #region Construtor
        public RepositorioEmprestimo(string urlBase) : base(urlBase)
        {
        }
        #endregion

        #region IRepositorioEmprestimo
        public IReadOnlyCollection<Emprestimo> ListarEmprestimosParticipante(int participanteId)
        {
            var response = Get($"emprestimos/participante/{participanteId}");
            var content = response.Content.ReadAsStringAsync().Result;
            var emprestimos = JsonConvert.DeserializeObject<IReadOnlyCollection<Emprestimo>>(content);

            return emprestimos;
        }
        #endregion
    }
}
