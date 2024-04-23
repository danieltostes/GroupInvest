using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Repositorios
{
    public class RepositorioAdesao : EFRepositorio<int, Adesao>, IRepositorioAdesao
    {
        #region Construtor
        public RepositorioAdesao(DbContext context) : base(context)
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

        public Adesao ObterAdesao(int participanteId, int periodoId)
        {
            return db.Set<Adesao>().Where(ades => ades.Participante.Id == participanteId && ades.Periodo.Id == periodoId)
                .Include(ades => ades.Participante)
                .Include(ades => ades.Periodo)
                .FirstOrDefault();
        }

        public IReadOnlyCollection<Adesao> ListarAdesoesPeriodo(Periodo periodo)
        {
            return db.Set<Adesao>().Where(
                ades => ades.Periodo.Id.Equals(periodo.Id))
                .Include(ades => ades.Participante)
                .Include(ades => ades.Periodo)
                .ToList();
        }

        public int ObterTotalCotasPeriodo(Periodo periodo)
        {
            return db.Set<Adesao>().Where(ades => ades.Periodo.Id.Equals(periodo.Id))
                .Sum(ades => ades.NumeroCotas);
        }
        #endregion
    }
}
