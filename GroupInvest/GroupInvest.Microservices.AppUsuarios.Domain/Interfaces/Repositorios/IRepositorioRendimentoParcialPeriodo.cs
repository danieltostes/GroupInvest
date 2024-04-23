using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para o repositorio de rendimento parcial do periodo
    /// </summary>
    public interface IRepositorioRendimentoParcialPeriodo : IMongoRepositorio<RendimentoParcialPeriodo>
    {
        /// <summary>
        /// Obtém o rendimento parcial de um período pela data de referência
        /// </summary>
        /// <param name="dataReferencia">Data de referência</param>
        /// <returns>Rendimento parcial do período</returns>
        RendimentoParcialPeriodo ObterRendimentoParcialPeriodoDataReferencia(DateTime dataReferencia);

        /// <summary>
        /// Lista os rendimentos parciais no periodo
        /// </summary>
        /// <returns></returns>
        IReadOnlyCollection<RendimentoParcialPeriodo> ListarRendimentosParciais();
    }
}
