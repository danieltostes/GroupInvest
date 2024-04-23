using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para repositórios de mensalidades
    /// </summary>
    public interface IRepositorioMensalidade : IRepositorio<int, Mensalidade>
    {
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
        /// <param name="periodo">Período desejado</param>
        /// <returns>Previsão de recebimento de mensalidades</returns>
        decimal ObterPrevisaoRecebimentoMensalidades(DateTime dataReferencia, Periodo periodo);

        /// <summary>
        /// Obtém o valor total devido das mensalidades em uma data de referência
        /// </summary>
        /// <param name="dataReferencia">Data de referência</param>
        /// <param name="periodo">Período desejado</param>
        /// <returns>Valor total devido das mensalidades</returns>
        decimal ObterValorTotalDevidoMensalidades(DateTime dataReferencia, Periodo periodo);

        /// <summary>
        /// Obtém o saldo devedor de uma adesão na data de referência
        /// </summary>
        /// <param name="adesao">Adesão</param>
        /// <param name="dataReferencia">Data de referência</param>
        /// <returns>Saldo devedor</returns>
        decimal ObterSaldoDevedorMensalidades(Adesao adesao, DateTime dataReferencia);

        /// <summary>
        /// Obtém o saldo devedor de uma adesão
        /// </summary>
        /// <param name="adesao">Adesão</param>
        /// <param name="dataReferencia">Data de referência</param>
        /// <returns>Saldo devedor</returns>
        decimal ObterSaldoDevedorAdesao(Adesao adesao, DateTime dataReferencia);
    }
}
