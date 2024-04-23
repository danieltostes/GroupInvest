using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GroupInvest.Microservices.Participantes.Infra.DataAccess.Repositorios
{
    public class RepositorioAdesao : EFRepositorio<int, Adesao>, IRepositorioAdesao
    {
        #region Construtor
        public RepositorioAdesao(DbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region IRepositorioAdesao
        public override Adesao ObterPorId(int id)
        {
            return db.Set<Adesao>()
                .Where(ades => ades.Id.Equals(id))
                .Include(ades => ades.Participante)
                .Include(ades => ades.Periodo)
                .FirstOrDefault();
        }
        public Adesao ObterAdesao(Participante participante, Periodo periodo)
        {
            if (!db.Set<Adesao>().Any())
                return null;

            return db.Set<Adesao>()
                .Where(ades => ades.Participante.Id.Equals(participante.Id) && ades.Periodo.Id.Equals(periodo.Id))
                .Include(ades => ades.Participante)
                .Include(ades => ades.Periodo)
                .FirstOrDefault();
        }

        public IReadOnlyCollection<Adesao> ListarTodos()
        {
            if (!db.Set<Adesao>().Any())
                return new List<Adesao>();

            return db.Set<Adesao>().ToList();
        }

        public IReadOnlyCollection<Adesao> ListarAdesoesAtivasPeriodo(Periodo periodo)
        {
            if (!db.Set<Adesao>().Any())
                return new List<Adesao>();

            return db.Set<Adesao>()
                .Where(ades => ades.Periodo.Id == periodo.Id && !ades.DataCancelamento.HasValue)
                .Include(ades => ades.Participante)
                .ToList();
        }
        #endregion
    }
}
