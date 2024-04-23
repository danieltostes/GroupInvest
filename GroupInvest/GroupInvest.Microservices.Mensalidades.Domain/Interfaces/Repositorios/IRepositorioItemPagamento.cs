using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para os repositórios de itens de pagamento
    /// </summary>
    public interface IRepositorioItemPagamento : IRepositorio<int, ItemPagamento>
    {
        /// <summary>
        /// Obtém o valor total recebido com pagamentos de mensalidades
        /// </summary>
        /// <param name="dataReferencia">Data de referência</param>
        /// <param name="periodo">Período desejado</param>
        /// <returns>Valor total recebido</returns>
        decimal ObterValorTotalRecebidoMensalidades(DateTime dataReferencia, Periodo periodo);

        /// <summary>
        /// Obtém o valor total recebido com pagamentos de empréstimos
        /// </summary>
        /// <param name="dataReferencia">Data de referência</param>
        /// <param name="periodo">Período desejado</param>
        /// <returns>Valor total recebido</returns>
        decimal ObterValorTotalRecebidoEmprestimos(DateTime dataReferencia, Periodo periodo);
    }
}
