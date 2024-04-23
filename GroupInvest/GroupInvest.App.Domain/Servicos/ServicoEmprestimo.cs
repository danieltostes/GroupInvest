using GroupInvest.App.Domain.Entidades;
using GroupInvest.App.Domain.Interfaces.Repositorios;
using GroupInvest.App.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Domain.Servicos
{
    public class ServicoEmprestimo : IServicoEmprestimo
    {
        public string Token { get; set; }

        #region Injeção de dependência
        private readonly IRepositorioEmprestimo repositorioEmprestimo;
        #endregion

        #region Construtor
        public ServicoEmprestimo(IRepositorioEmprestimo repositorioEmprestimo)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
        }
        #endregion

        #region IServicoEmprestimo
        public IReadOnlyCollection<Emprestimo> ListarEmprestimosParticipante(int participanteId)
        {
            repositorioEmprestimo.Token = Token;
            return repositorioEmprestimo.ListarEmprestimosParticipante(participanteId);
        }
        #endregion
    }
}
