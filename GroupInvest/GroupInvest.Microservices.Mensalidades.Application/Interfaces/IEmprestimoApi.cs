using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Models.Dtos;
using System;
using System.Collections.Generic;

namespace GroupInvest.Microservices.Mensalidades.Application.Interfaces
{
    /// <summary>
    /// Interface para api de empréstimos
    /// </summary>
    public interface IEmprestimoApi
    {
        /// <summary>
        /// Concede empréstimo a um participante
        /// </summary>
        /// <param name="dto">Dto para concessão de empréstimo</param>
        /// <returns>Resultado da operação</returns>
        OperationResult ConcederEmprestimo(ConcessaoEmprestimoDto dto);

        /// <summary>
        /// Atualiza o saldo dos empréstimos e envia o evento para a integração com o microsserviço do aplicativo
        /// </summary>
        /// <param name="dataReferencia">Data de referência</param>
        /// <returns>Resultado da operação</returns>
        OperationResult AtualizarSaldoEmprestimosIntegracao(DateTime dataReferencia);

        /// <summary>
        /// Obtém o empréstimo de um participante pela data
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <param name="dataEmprestimo">Data do empréstimo</param>
        /// <returns>Empréstimo</returns>
        EmprestimoDto ObterEmprestimoParticipante(int participanteId, DateTime dataEmprestimo);

        /// <summary>
        /// Lista todos os empréstimos de um participante
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Lista de empréstimos</returns>
        IReadOnlyCollection<EmprestimoDto> ListarEmprestimosParticipante(int participanteId);

        /// <summary>
        /// Lista todos os empréstimos do período ativo
        /// </summary>
        /// <returns>Lista de empréstimos</returns>
        IReadOnlyCollection<EmprestimoDto> ListarTodosEmprestimos();

        /// <summary>
        /// Lista todas as previsões de pagamento de um empréstimo
        /// </summary>
        /// <param name="emprestimoId">Identificador do empréstimo</param>
        /// <returns>Lista de previsões de pagamento</returns>
        IReadOnlyCollection<PrevisaoPagamentoEmprestimoDto> ListarPrevisoesPagamentoEmprestimo(int emprestimoId);
    }
}
