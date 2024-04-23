using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para o repositório de empréstimos
    /// </summary>
    public interface IRepositorioEmprestimo : IMongoRepositorio<Emprestimo>
    {
        /// <summary>
        /// Adiciona um pagamento 
        /// </summary>
        /// <param name="emprestimo">Empréstimo</param>
        /// <param name="pagamento">Pagamento</param>
        void AdicionarPagamento(Emprestimo emprestimo, PagamentoEmprestimo pagamento);

        /// <summary>
        /// Atualiza o saldo de um empréstimo
        /// </summary>
        /// <param name="emprestimo">Empréstimo</param>
        void AtualizarSaldoEmprestimo(Emprestimo emprestimo);

        /// <summary>
        /// Atualiza as informações do próximo pagamento do empréstimo
        /// </summary>
        /// <param name="emprestimo">Empréstimo</param>
        void AtualizarProximoPagamentoEmprestimo(Emprestimo emprestimo);

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
