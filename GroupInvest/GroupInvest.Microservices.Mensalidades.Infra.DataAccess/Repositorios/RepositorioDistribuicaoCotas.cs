using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Repositorios
{
    public class RepositorioDistribuicaoCotas : EFRepositorio<int, DistribuicaoCotas>, IRepositorioDistribuicaoCotas
    {
        #region Construtor
        public RepositorioDistribuicaoCotas(DbContext context): base(context)
        {
        }
        #endregion
    }
}
