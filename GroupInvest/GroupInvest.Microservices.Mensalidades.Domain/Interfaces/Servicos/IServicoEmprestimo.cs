using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos
{
    /// <summary>
    /// Interface para o serviço de empréstimos
    /// </summary>
    public interface IServicoEmprestimo
    {
        /// <summary>
        /// Concede um empréstimo para o participante no período ativo
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <param name="data">Data do empréstimo</param>
        /// <param name="valor">Valor do empréstimo</param>
        /// <returns>Resultado da operação</returns>
        OperationResult ConcederEmprestimo(Participante participante, DateTime data, decimal valor);

        /// <summary>
        /// Atualiza as previsões de pagamento de um empréstimo
        /// </summary>
        /// <param name="emprestimo">Empréstimo</param>
        /// <param name="dataReferencia">Data de referência</param>
        /// <returns>Resultado da operação</returns>
        OperationResult AtualizarPrevisaoPagamentoEmprestimo(Emprestimo emprestimo, DateTime dataReferencia);

        /// <summary>
        /// Obtém um empréstimo concedido a um participante pela data do empréstimo
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <param name="dataEmprestimo">Data de concessão do empréstimo</param>
        /// <returns>Empréstimo</returns>
        Emprestimo ObterEmprestimoParticipante(Participante participante, DateTime dataEmprestimo);

        /// <summary>
        /// Obtém um empréstimo através do seu identificador
        /// </summary>
        /// <param name="emprestimoId">Identificador do empréstimo</param>
        /// <returns>Empréstimo</returns>
        Emprestimo ObterEmprestimoPorId(int emprestimoId);

        /// <summary>
        /// Obtém uma previsão de pagamento de empréstimo através do identificador
        /// </summary>
        /// <param name="previsaoPagamentoEmprestimoId">Identificador da previsão de pagamento de empréstimo</param>
        /// <returns>Previsão de pagamento de empréstimo</returns>
        PrevisaoPagamentoEmprestimo ObterPrevisaoPagamentoEmprestimoPorId(int previsaoPagamentoEmprestimoId);

        /// <summary>
        /// Lista todos os empréstimos de um participante
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <returns>Lista de empréstimos</returns>
        IReadOnlyCollection<Emprestimo> ListarEmprestimosParticipante(Participante participante);

        /// <summary>
        /// Lista todos os empréstimos do período ativo
        /// </summary>
        /// <returns>Lista de empréstimos</returns>
        IReadOnlyCollection<Emprestimo> ListarTodosEmprestimos();

        /// <summary>
        /// Lista todos os empréstimos em aberto do período ativo
        /// </summary>
        /// <returns></returns>
        IReadOnlyCollection<Emprestimo> ListarEmprestimosEmAberto();

        /// <summary>
        /// Lista as previsões de pagamento de um empréstimo
        /// </summary>
        /// <param name="emprestimo">Empréstimo</param>
        /// <param name="apenasPendentes">Indicador para listar apenas previsões pendentes</param>
        /// <returns>Lista de previsões de pagamento</returns>
        IReadOnlyCollection<PrevisaoPagamentoEmprestimo> ListarPrevisoesPagamentoEmprestimo(Emprestimo emprestimo, bool apenasPendentes);

        /// <summary>
        /// Obtém o valor previso para receber com todos os empréstimos
        /// </summary>
        /// <param name="dataReferencia">Data de referência</param>
        /// <returns>Valor total a receber</returns>
        OperationResult ObterValorTotalReceberEmprestimos(DateTime dataReferencia);

        /// <summary>
        /// Obtém o saldo devedor de um participante
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <returns>Saldo devedor</returns>
        decimal ObterSaldoDevedorParticipante(Participante participante);
    }
}
