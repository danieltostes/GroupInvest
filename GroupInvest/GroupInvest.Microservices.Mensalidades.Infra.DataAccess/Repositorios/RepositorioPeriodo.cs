using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Repositorios
{
    public class RepositorioPeriodo : EFRepositorio<int, Periodo>, IRepositorioPeriodo
    {
        #region Construtor
        public RepositorioPeriodo(DbContext context) : base(context)
        {
        }
        #endregion

        #region IRepositorioPeriodo
        public Periodo ObterPeriodoAtivo()
        {
            //if (!db.Set<Periodo>().Any())
            //    return null;

            return db.Set<Periodo>().FirstOrDefault(p => p.Ativo == true);
        }
        #endregion
    }
}
