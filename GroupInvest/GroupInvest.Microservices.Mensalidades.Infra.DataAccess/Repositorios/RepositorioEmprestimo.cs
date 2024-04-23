using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Repositorios
{
    public class RepositorioEmprestimo : EFRepositorio<int, Emprestimo>, IRepositorioEmprestimo
    {
        #region Construtor
        public RepositorioEmprestimo(DbContext context) : base(context)
        {
        }
        #endregion

        #region IRepositorioEmprestimo
        public override Emprestimo ObterPorId(int id)
        {
            return db.Set<Emprestimo>()
                .Where(emp => emp.Id.Equals(id))
                .Include(emp => emp.Adesao)
                .Include(emp => emp.Adesao.Participante)
                .FirstOrDefault();
        }

        public Emprestimo ObterEmprestimo(Adesao adesao, DateTime dataEmprestimo)
        {
            return db.Set<Emprestimo>()
                .Where(emp => emp.Adesao.Id.Equals(adesao.Id) && emp.Data.Equals(dataEmprestimo))
                .Include(emp => emp.Adesao)
                .Include(emp => emp.Adesao.Participante)
                .FirstOrDefault();
        }

        public IReadOnlyCollection<Emprestimo> ListarEmprestimosAdesao(Adesao adesao)
        {
            return db.Set<Emprestimo>()
                .Where(emp => emp.Adesao.Id.Equals(adesao.Id))
                .Include(emp => emp.Adesao)
                .Include(emp => emp.Adesao.Participante)
                .ToList();
        }

        public IReadOnlyCollection<Emprestimo> ListarEmprestimosPeriodo(Periodo periodo)
        {
            return db.Set<Emprestimo>()
                .Where(emp => emp.Adesao.Periodo.Id.Equals(periodo.Id))
                .Include(emp => emp.Adesao)
                .Include(emp => emp.Adesao.Participante)
                .ToList();
        }

        public IReadOnlyCollection<Emprestimo> ListarEmprestimosEmAbertoPeriodo(Periodo periodo)
        {
            return db.Set<Emprestimo>()
                .Where(emp => emp.Adesao.Periodo.Id.Equals(periodo.Id) && emp.Quitado == false)
                .Include(emp => emp.Adesao)
                .Include(emp => emp.Adesao.Participante)
                .ToList();
        }

        public decimal ObterSaldoDevedorAdesao(Adesao adesao)
        {
            return db.Set<Emprestimo>()
                .Where(emp => emp.Adesao.Id.Equals(adesao.Id))
                .Sum(emp => emp.Saldo);
        }
        #endregion
    }
}
