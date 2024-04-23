using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Interfaces
{
    /// <summary>
    /// Interface para a api de pagamentos
    /// </summary>
    public interface IPagamentoApi
    {
        /// <summary>
        /// Realiza um pagamento
        /// </summary>
        /// <param name="pagamento">Pagamento</param>
        /// <returns>Resultado da operação</returns>
        OperationResult RealizarPagamento(PagamentoDto pagamento);

        /// <summary>
        /// Realiza um pagamento retroativo
        /// </summary>
        /// <param name="pagamento">Pagamento</param>
        /// <returns>Resultado da operação</returns>
        OperationResult RealizarPagamentoRetroativo(PagamentoDto pagamento);
    }
}
