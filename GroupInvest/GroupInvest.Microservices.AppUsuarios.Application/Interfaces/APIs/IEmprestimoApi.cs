using GroupInvest.Microservices.AppUsuarios.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Interfaces.APIs
{
    /// <summary>
    /// Interface para a API de empréstimos
    /// </summary>
    public interface IEmprestimoApi
    {
        /// <summary>
        /// Lista os empréstimos de um participante
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Lista de empréstimos</returns>
        IReadOnlyCollection<EmprestimoDto> ListarEmprestimosParticipante(int participanteId);

        /// <summary>
        /// Obtém um empréstimo
        /// </summary>
        /// <param name="emprestimoId">Identificador do empréstimo</param>
        /// <returns>Empréstimo</returns>
        EmprestimoDto ObterEmprestimo(int emprestimoId);
    }
}
