using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para os repositórios de distribuição de cotas
    /// </summary>
    public interface IRepositorioDistribuicaoCotas : IRepositorio<int, DistribuicaoCotas>
    {
    }
}
