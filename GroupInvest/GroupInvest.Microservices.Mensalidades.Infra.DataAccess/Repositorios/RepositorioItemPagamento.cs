using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Repositorios
{
    public class RepositorioItemPagamento : EFRepositorio<int, ItemPagamento>, IRepositorioItemPagamento
    {
        #region Construtor
        public RepositorioItemPagamento(DbContext context) : base(context)
        {
        }
        #endregion

        #region IRepositorioItemPagamento
        public decimal ObterValorTotalRecebidoMensalidades(DateTime dataReferencia, Periodo periodo)
        {
            if (!db.Set<ItemPagamento>().Any())
                return 0;

            return db.Set<ItemPagamento>().Where(it =>
                it.Mensalidade != null &&
                it.Mensalidade.Adesao.Periodo.Id.Equals(periodo.Id) &&
                it.Pagamento.DataPagamento <= dataReferencia)
                .Sum(it => it.Valor);
        }

        public decimal ObterValorTotalRecebidoEmprestimos(DateTime dataReferencia, Periodo periodo)
        {
            if (!db.Set<ItemPagamento>().Any())
                return 0;

            return db.Set<ItemPagamento>().Where(it =>
                it.PrevisaoPagamentoEmprestimo != null &&
                it.PrevisaoPagamentoEmprestimo.Emprestimo.Adesao.Periodo.Id.Equals(periodo.Id) &&
                it.Pagamento.DataPagamento <= dataReferencia)
                .Sum(it => it.Valor);
        }
        #endregion
    }
}
