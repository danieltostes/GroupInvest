using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Domain.Classes;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;

namespace GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos
{
    /// <summary>
    /// Interface para o serviço de pagamentos
    /// </summary>
    public interface IServicoPagamento
    {
        /// <summary>
        /// Realiza um pagamento
        /// </summary>
        /// <param name="fatura">Fatura que representa o conjunto de itens pagos</param>
        /// <returns>Resultado da operação</returns>
        OperationResult RealizarPagamento(Fatura fatura);

        /// <summary>
        /// Realiza um pagamento retroativo de uma fatura
        /// </summary>
        /// <param name="fatura">Fatura que representa o conjunto de itens pagos</param>
        /// <returns>Resultado da operação</returns>
        OperationResult RealizarPagamentoRetroativo(Fatura fatura);

        /// <summary>
        /// Obtém o valor total recebido com pagamentos de mensalidades
        /// </summary>
        /// <param name="dataReferencia">Data de referência</param>
        /// <returns>Valor total recebido</returns>
        decimal ObterValorTotalRecebidoMensalidades(DateTime dataReferencia);
        
        /// <summary>
        /// Obtém o valor total recebido com pagamentos de empréstimos
        /// </summary>
        /// <param name="dataReferencia">Data de referência</param>
        /// <returns>Valor total recebido</returns>
        decimal ObterValorTotalRecebidoEmprestimos(DateTime dataReferencia);
    }
}
