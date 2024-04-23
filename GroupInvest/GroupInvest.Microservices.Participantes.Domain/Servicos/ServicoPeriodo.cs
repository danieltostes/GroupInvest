using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Participantes.Domain.Resources;
using GroupInvest.Microservices.Participantes.Domain.Validations.Periodos;
using System.Collections.Generic;

namespace GroupInvest.Microservices.Participantes.Domain.Servicos
{
    public class ServicoPeriodo : IServicoPeriodo
    {
        private readonly IRepositorioPeriodo repositorioPeriodo;
        private readonly IRepositorioAdesao repositorioAdesao;

        #region Construtor
        public ServicoPeriodo(IRepositorioPeriodo repositorioPeriodo, IRepositorioAdesao repositorioAdesao)
        {
            this.repositorioPeriodo = repositorioPeriodo;
            this.repositorioAdesao = repositorioAdesao;
        }
        #endregion

        #region IServicoPeriodo
        public OperationResult IncluirPeriodo(Periodo periodo)
        {
            if (periodo == null)
                return new OperationResult(StatusCodeEnum.Error, ResourceHelper.Get("PeriodoNulo"));

            var validation = new PersistenciaPeriodoValidation(repositorioPeriodo);
            if (validation.IsValid(periodo))
            {
                repositorioPeriodo.Incluir(periodo);

                if (repositorioPeriodo.Commit() > 0) return OperationResult.OK;
                else return new OperationResult(StatusCodeEnum.Error, ResourceHelper.Get("CommitError"));
            }
            else
                return new OperationResult(StatusCodeEnum.Error, validation.Messages);
        }

        public OperationResult AlterarPeriodo(Periodo periodo)
        {
            if (periodo == null)
                return new OperationResult(StatusCodeEnum.Error, ResourceHelper.Get("PeriodoNulo"));

            var validation = new PersistenciaPeriodoValidation(repositorioPeriodo);
            if (validation.IsValid(periodo))
            {
                repositorioPeriodo.Alterar(periodo);

                if (repositorioPeriodo.Commit() > 0) return OperationResult.OK;
                else return new OperationResult(StatusCodeEnum.Error, ResourceHelper.Get("CommitError"));
            }
            else
                return new OperationResult(StatusCodeEnum.Error, validation.Messages);
        }

        public OperationResult ExcluirPeriodo(Periodo periodo)
        {
            if (periodo == null)
                return new OperationResult(StatusCodeEnum.Error, ResourceHelper.Get("PeriodoNulo"));

            var validation = new ExclusaoPeriodoValidation(repositorioAdesao);
            if (validation.IsValid(periodo))
            {
                repositorioPeriodo.Excluir(periodo);

                if (repositorioPeriodo.Commit() > 0) return OperationResult.OK;
                else return new OperationResult(StatusCodeEnum.Error, ResourceHelper.Get("CommitError"));
            }
            else return new OperationResult(StatusCodeEnum.Error, validation.Messages);
        }

        public OperationResult EncerrarPeriodo(Periodo periodo)
        {
            return new OperationResult(StatusCodeEnum.Error, "Implementar encerramento do período considerando distribuição de cotas");
        }

        public Periodo ObterPeriodoAtivo()
        {
            return repositorioPeriodo.ObterPeriodoAtivo();
        }

        public Periodo ObterPeriodoPorAnoReferencia(int anoReferencia)
        {
            return repositorioPeriodo.ObterPeriodoPorAnoReferencia(anoReferencia);
        }

        public IReadOnlyCollection<Adesao> ListarAdesoesAtivasPeriodo(Periodo periodo)
        {
            return repositorioAdesao.ListarAdesoesAtivasPeriodo(periodo);
        }
        #endregion
    }
}
