using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos
{
    /// <summary>
    /// Interface para os serviços de mensalidades
    /// </summary>
    public interface IServicoMensalidade
    {
        /// <summary>
        /// Gera as mensalidades do participante no período
        /// </summary>
        /// <param name="adesao">Adesão</param>
        /// <returns>Resultado da operação</returns>
        OperationResult GerarMensalidades(Adesao adesao);

        /// <summary>
        /// Obtém uma mensalidade pelo seu identificador
        /// </summary>
        /// <param name="id">Identificador da mensalidade</param>
        /// <returns>Mensalidade</returns>
        Mensalidade ObterMensalidadePorId(int id);

        /// <summary>
        /// Lista as mensalidades de uma adesão
        /// </summary>
        /// <param name="adesao">Adesão</param>
        /// <returns>Lista de mensalidades</returns>
        IReadOnlyCollection<Mensalidade> ListarMensalidadesAdesao(Adesao adesao);

        /// <summary>
        /// Obtém a previsão de recebimento de mensalidades em uma data de referência
        /// </summary>
        /// <param name="dataReferencia">Data de referência</param>
        /// <returns>Previsão de recebimento de mensalidades</returns>
        decimal ObterPrevisaoRecebimentoMensalidades(DateTime dataReferencia);

        /// <summary>
        /// Obtém o valor total devido das mensalidades em uma data de referência
        /// </summary>
        /// <param name="dataReferencia">Data de referência</param>
        /// <returns>Valor total devido das mensalidades</returns>
        decimal ObterValorTotalDevidoMensalidades(DateTime dataReferencia);

        /// <summary>
        /// Obtém o saldo devedor de uma adesão na data de referência
        /// </summary>
        /// <param name="adesao">Adesão</param>
        /// <param name="dataReferencia">Data de referência</param>
        /// <returns>Saldo devedor</returns>
        decimal ObterSaldoDevedorMensalidades(Adesao adesao, DateTime dataReferencia);
    }
}
