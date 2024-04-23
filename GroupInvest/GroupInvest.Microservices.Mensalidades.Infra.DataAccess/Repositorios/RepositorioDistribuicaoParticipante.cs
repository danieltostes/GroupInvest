using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Repositorios
{
    public class RepositorioDistribuicaoParticipante : EFRepositorio<int, DistribuicaoParticipante>, IRepositorioDistribuicaoParticipante
    {
        #region Construtor
        public RepositorioDistribuicaoParticipante(DbContext context): base(context)
        {
        }
        #endregion
    }
}
