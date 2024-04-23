using GroupInvest.App.Domain.Entidades;
using GroupInvest.App.Domain.Interfaces.Repositorios;
using GroupInvest.App.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Domain.Servicos
{
    public class ServicoMensalidade : IServicoMensalidade
    {
        public string Token { get; set; }

        #region Injeção de dependência
        private readonly IRepositorioMensalidade repositorioMensalidade;
        #endregion

        #region Construtor
        public ServicoMensalidade(IRepositorioMensalidade repositorioMensalidade)
        {
            this.repositorioMensalidade = repositorioMensalidade;
        }
        #endregion

        #region IServicoMensalidade
        public IReadOnlyCollection<Mensalidade> ListarMensalidadesParticipante(int participanteId)
        {
            repositorioMensalidade.Token = this.Token;
            return repositorioMensalidade.ListarMensalidadesParticipante(participanteId);
        }
        #endregion
    }
}
