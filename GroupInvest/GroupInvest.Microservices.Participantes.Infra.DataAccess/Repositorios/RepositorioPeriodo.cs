using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GroupInvest.Microservices.Participantes.Infra.DataAccess.Repositorios
{
    public class RepositorioPeriodo : EFRepositorio<int, Periodo>, IRepositorioPeriodo
    {
        #region Construtor
        public RepositorioPeriodo(DbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region IRepositorioPeriodo
        public Periodo ObterPeriodoAtivo()
        {
            if (!db.Set<Periodo>().Any())
                return null;

            return db.Set<Periodo>().FirstOrDefault(p => p.Ativo == true);
        }

        public Periodo ObterPeriodoPorAnoReferencia(int anoReferencia)
        {
            if (!db.Set<Periodo>().Any())
                return null;

            return db.Set<Periodo>().FirstOrDefault(p => p.AnoReferencia.Equals(anoReferencia));
        }

        public IReadOnlyCollection<Periodo> ListarTodos()
        {
            if (!db.Set<Periodo>().Any())
                return new List<Periodo>();

            return db.Set<Periodo>().ToList();
        }
        #endregion
    }
}
