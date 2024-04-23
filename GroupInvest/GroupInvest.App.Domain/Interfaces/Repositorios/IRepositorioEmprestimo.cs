using GroupInvest.App.Domain.Entidades;
using GroupInvest.Common.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Domain.Interfaces.Repositorios
{
    public interface IRepositorioEmprestimo : IHttpRepositorio<Emprestimo>
    {
        /// <summary>
        /// Token para as requisições http
        /// </summary>
        string Token { get; set; }

        /// <summary>
        /// Lista os empréstimos de um participante
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Lista de empréstimos</returns>
        IReadOnlyCollection<Emprestimo> ListarEmprestimosParticipante(int participanteId);
    }
}
