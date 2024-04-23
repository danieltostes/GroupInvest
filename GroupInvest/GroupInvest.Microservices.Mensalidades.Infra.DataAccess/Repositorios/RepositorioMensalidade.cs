using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Repositorios
{
    public class RepositorioMensalidade : EFRepositorio<int, Mensalidade>, IRepositorioMensalidade
    {
        #region Construtor
        public RepositorioMensalidade(DbContext context) : base(context)
        {
        }
        #endregion

        #region IRepositorioMensalidade
        public override Mensalidade ObterPorId(int id)
        {
            return db.Set<Mensalidade>().Where(mens => mens.Id.Equals(id))
                .Include(mens => mens.Adesao)
                .Include(mens => mens.Adesao.Participante)
                .Include(mens => mens.Adesao.Periodo)
                .FirstOrDefault();
        }

        public IReadOnlyCollection<Mensalidade> ListarMensalidadesAdesao(Adesao adesao)
        {
            return db.Set<Mensalidade>().Where(mens => mens.Adesao.Id == adesao.Id)
                .Include(mens => mens.Adesao)
                .Include(mens => mens.Adesao.Participante)
                .Include(mens => mens.Adesao.Periodo)
                .ToList();
        }

        public decimal ObterPrevisaoRecebimentoMensalidades(DateTime dataReferencia, Periodo periodo)
        {
            if (!db.Set<Mensalidade>().Any())
                return 0;

            var previsaoRecebimento = db.Set<Mensalidade>()
                .Where(mens => mens.Adesao.Periodo.Id.Equals(periodo.Id) &&
                               mens.DataVencimento <= dataReferencia)
                .Sum(mens => mens.ValorBase);

            return previsaoRecebimento;
        }

        public decimal ObterValorTotalDevidoMensalidades(DateTime dataReferencia, Periodo periodo)
        {
            if (!db.Set<Mensalidade>().Any())
                return 0;

            var mensalidades = db.Set<Mensalidade>().Where(mens =>
                mens.Adesao.Periodo.Id.Equals(periodo.Id) &&
                mens.DataVencimento <= dataReferencia);

            var valorMensalidadesPagas = mensalidades.Where(mens => mens.DataPagamento.HasValue).Sum(mens => mens.ValorDevido);
            var valorMensalidadesEmAberto = mensalidades.Where(mens => !mens.DataPagamento.HasValue).Sum(mens => mens.ValorBase * (1 + (Parametrizacao.PercentualJurosMensalidades / 100)));

            return valorMensalidadesPagas + valorMensalidadesEmAberto;
        }

        public decimal ObterSaldoDevedorMensalidades(Adesao adesao, DateTime dataReferencia)
        {
            if (!db.Set<Mensalidade>().Any())
                return 0;

            return db.Set<Mensalidade>().Where(
                mens => mens.Adesao.Id.Equals(adesao.Id) &&
                mens.DataVencimento <= dataReferencia &&
                mens.DataPagamento == null)
                .Sum(mens => mens.ValorBase * (1 + (Parametrizacao.PercentualJurosMensalidades / 100)));
        }

        public decimal ObterSaldoDevedorAdesao(Adesao adesao, DateTime dataReferencia)
        {
            if (!db.Set<Mensalidade>().Any())
                return 0;

            return db.Set<Mensalidade>()
                .Where(mens =>
                    mens.Adesao.Id.Equals(adesao.Id) &&
                    mens.DataVencimento <= dataReferencia &&
                    !mens.DataPagamento.HasValue)
                .Sum(mens => mens.ValorBase * (1 + (Parametrizacao.PercentualJurosMensalidades / 100)));
        }
        #endregion
    }
}
