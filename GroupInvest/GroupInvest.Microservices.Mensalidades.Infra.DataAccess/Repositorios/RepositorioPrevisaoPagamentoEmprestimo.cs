using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Repositorios
{
    public class RepositorioPrevisaoPagamentoEmprestimo : EFRepositorio<int, PrevisaoPagamentoEmprestimo>, IRepositorioPrevisaoPagamentoEmprestimo
    {
        #region Construtor
        public RepositorioPrevisaoPagamentoEmprestimo(DbContext context) : base(context)
        {
        }
        #endregion

        #region Overrides
        public override PrevisaoPagamentoEmprestimo ObterPorId(int id)
        {
            if (!db.Set<PrevisaoPagamentoEmprestimo>().Any())
                return null;

            return db.Set<PrevisaoPagamentoEmprestimo>().Where(prev => prev.Id.Equals(id))
                .Include(prev => prev.Emprestimo)
                .FirstOrDefault();
        }
        #endregion

        #region IRepositorioPrevisaoPagamentoEmprestimo
        public IReadOnlyCollection<PrevisaoPagamentoEmprestimo> ListarPrevisoesPagamentoEmprestimo(Emprestimo emprestimo, bool apenasPendentes)
        {
            if (!db.Set<PrevisaoPagamentoEmprestimo>().Any())
                return new List<PrevisaoPagamentoEmprestimo>();

            var previsoes = db.Set<PrevisaoPagamentoEmprestimo>()
                .Where(prev => prev.Emprestimo.Id.Equals(emprestimo.Id))
                .Include(prev => prev.Emprestimo);

            if (apenasPendentes)
            {
                return previsoes.Where(prev => prev.Realizada == false)
                    .OrderBy(prev => prev.DataVencimento)
                    .ToList();
            }
            else
                return previsoes.OrderBy(prev => prev.DataVencimento).ToList();
        }

        public IReadOnlyCollection<PrevisaoPagamentoEmprestimo> ListarPrevisoesPagamentoEmprestimo(Periodo periodo, DateTime dataReferencia)
        {
            if (!db.Set<PrevisaoPagamentoEmprestimo>().Any())
                return new List<PrevisaoPagamentoEmprestimo>();

            var previsoes = db.Set<PrevisaoPagamentoEmprestimo>()
                .Where(prev => prev.Emprestimo.Adesao.Periodo.Id.Equals(periodo.Id) && prev.DataVencimento <= dataReferencia)
                .ToList();

            return previsoes;
        }
        #endregion
    }
}
