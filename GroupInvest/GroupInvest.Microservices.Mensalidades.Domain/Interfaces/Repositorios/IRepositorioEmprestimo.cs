using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para repositórios de empréstimos
    /// </summary>
    public interface IRepositorioEmprestimo : IRepositorio<int, Emprestimo>
    {
        /// <summary>
        /// Obtém o empréstimo concedido a um participante na data solicitada
        /// </summary>
        /// <param name="adesao">Adesão</param>
        /// <param name="dataEmprestimo">Data de concessão do empréstimo</param>
        /// <returns>Empréstimo</returns>
        Emprestimo ObterEmprestimo(Adesao adesao, DateTime dataEmprestimo);

        /// <summary>
        /// Lista todos os empréstimos de uma adesão
        /// </summary>
        /// <param name="adesao">Adesão</param>
        /// <returns>Lista de empréstimos</returns>
        IReadOnlyCollection<Emprestimo> ListarEmprestimosAdesao(Adesao adesao);

        /// <summary>
        /// Lista todos os empréstimos de um período
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <returns>Lista de empréstimos</returns>
        IReadOnlyCollection<Emprestimo> ListarEmprestimosPeriodo(Periodo periodo);

        /// <summary>
        /// Lista todos os empréstimos em aberto de um período
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <returns>Lista de empréstimos</returns>
        IReadOnlyCollection<Emprestimo> ListarEmprestimosEmAbertoPeriodo(Periodo periodo);

        /// <summary>
        /// Obtém o saldo devedor de uma adesão
        /// </summary>
        /// <param name="adesao">Adesão</param>
        /// <returns>Saldo devedor do participante</returns>
        decimal ObterSaldoDevedorAdesao(Adesao adesao);
    }
}
