using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios
{
    public interface IRepositorioPrevisaoPagamentoEmprestimo : IRepositorio<int, PrevisaoPagamentoEmprestimo>
    {
        /// <summary>
        /// Lista as previsões de pagamento de um empréstimo que ainda não foram realizadas
        /// </summary>
        /// <param name="emprestimo">Empréstimo</param>
        /// <param name="apenasPendentes">Indicador para listar apenas previsões pendentes</param>
        /// <returns>Lista de previsões de pagamento</returns>
        IReadOnlyCollection<PrevisaoPagamentoEmprestimo> ListarPrevisoesPagamentoEmprestimo(Emprestimo emprestimo, bool apenasPendentes);

        /// <summary>
        /// Lista as previsões de pagamento de todos os empréstimos do periodo até a data de referência
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <param name="dataReferencia">Data de referência</param>
        /// <returns>Lista de previsões de pagamento de empréstimos</returns>
        IReadOnlyCollection<PrevisaoPagamentoEmprestimo> ListarPrevisoesPagamentoEmprestimo(Periodo periodo, DateTime dataReferencia);
    }
}
