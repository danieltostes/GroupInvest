using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Helpers;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Mensalidades.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupInvest.Microservices.Mensalidades.Domain.Servicos
{
    public class ServicoMensalidade : IServicoMensalidade
    {
        private readonly IRepositorioMensalidade repositorioMensalidade;
        private readonly IRepositorioPeriodo repositorioPeriodo;

        #region Construtor
        public ServicoMensalidade(IRepositorioMensalidade repositorioMensalidade, IRepositorioPeriodo repositorioPeriodo)
        {
            this.repositorioMensalidade = repositorioMensalidade;
            this.repositorioPeriodo = repositorioPeriodo;
        }
        #endregion

        #region IServicoMensalidade
        public OperationResult GerarMensalidades(Adesao adesao)
        {
            var mensalidades = new List<Mensalidade>();

            var dataReferencia = adesao.Periodo.DataInicioPeriodo.GetValueOrDefault();
            do
            {
                var dataVencimento = DateTimeHelper.ObterDataDiaUtil(new DateTime(dataReferencia.Year, dataReferencia.Month, Parametrizacao.DiaVencimentoMensalidade));

                var mensalidade = new Mensalidade
                {
                    Adesao = adesao,
                    DataReferencia = dataReferencia,
                    DataVencimento = dataVencimento,
                    ValorBase = adesao.NumeroCotas * Parametrizacao.ValorCota,
                    ValorDevido = adesao.NumeroCotas * Parametrizacao.ValorCota
                };
                repositorioMensalidade.Incluir(mensalidade);
                mensalidades.Add(mensalidade);
                dataReferencia = dataReferencia.AddMonths(1);

            } while (dataReferencia <= adesao.Periodo.DataTerminoPeriodo);

            if (repositorioMensalidade.Commit() > 0) return new OperationResult(mensalidades);
            else return new OperationResult(StatusCodeEnum.Error, Mensagens.CommitError);
        }

        public Mensalidade ObterMensalidadePorId(int id)
        {
            return repositorioMensalidade.ObterPorId(id);
        }

        public IReadOnlyCollection<Mensalidade> ListarMensalidadesAdesao(Adesao adesao)
        {
            return repositorioMensalidade.ListarMensalidadesAdesao(adesao);
        }

        public decimal ObterPrevisaoRecebimentoMensalidades(DateTime dataReferencia)
        {
            var periodoAtivo = repositorioPeriodo.ObterPeriodoAtivo();
            if (periodoAtivo == null)
                return 0;

            return repositorioMensalidade.ObterPrevisaoRecebimentoMensalidades(dataReferencia, periodoAtivo);
        }

        public decimal ObterValorTotalDevidoMensalidades(DateTime dataReferencia)
        {
            var periodoAtivo = repositorioPeriodo.ObterPeriodoAtivo();
            if (periodoAtivo == null)
                return 0;

            return repositorioMensalidade.ObterValorTotalDevidoMensalidades(dataReferencia, periodoAtivo);
        }


        public decimal ObterSaldoDevedorMensalidades(Adesao adesao, DateTime dataReferencia)
        {
            return repositorioMensalidade.ObterSaldoDevedorMensalidades(adesao, dataReferencia);
        }
        #endregion
    }
}
