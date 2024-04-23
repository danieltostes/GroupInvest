using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos
{
    /// <summary>
    /// Interface para serviços de empréstimo
    /// </summary>
    public interface IServicoEmprestimo
    {
        /// <summary>
        /// Inclui um empréstimo
        /// </summary>
        /// <param name="emprestimo">Empréstimo</param>
        /// <returns>Resultado da operação</returns>
        OperationResult IncluirEmprestimo(Emprestimo emprestimo);

        /// <summary>
        /// Altera um empréstimo
        /// </summary>
        /// <param name="emprestimo">Empréstimo</param>
        /// <returns>Resultado da operação</returns>
        OperationResult AlterarEmprestimo(Emprestimo emprestimo);

        /// <summary>
        /// Adiciona um pagmento a um empréstimo
        /// </summary>
        /// <param name="emprestimo">Empréstimo</param>
        /// <param name="pagamento">Pagamento</param>
        /// <returns>Resultado da operação</returns>
        OperationResult AdicionarPagamento(Emprestimo emprestimo, PagamentoEmprestimo pagamento);

        /// <summary>
        /// Atualiza o saldo de um empréstimo
        /// </summary>
        /// <param name="emprestimo">Empréstimo</param>
        /// <returns>Resultado da operação</returns>
        OperationResult AtualizarSaldoEmprestimo(Emprestimo emprestimo);

        /// <summary>
        /// Lista os empréstimos de um participante
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Lista de empréstimos</returns>
        IReadOnlyCollection<Emprestimo> ListarEmprestimosParticipante(int participanteId);

        /// <summary>
        /// Obtém um empréstimo pelo identificador
        /// </summary>
        /// <param name="id">Identificador do empréstimo</param>
        /// <returns>Empréstimo</returns>
        Emprestimo ObterEmprestimoPorId(int id);
    }
}
