using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;

namespace GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para os repositórios de período
    /// </summary>
    public interface IRepositorioPeriodo : IRepositorio<int, Periodo>
    {
        /// <summary>
        /// Obtém o período ativo
        /// </summary>
        /// <returns>Período ativo</returns>
        Periodo ObterPeriodoAtivo();
    }
}
