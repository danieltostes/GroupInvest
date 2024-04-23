using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Mensalidades.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Servicos
{
    public class ServicoAdesao : IServicoAdesao
    {
        #region Injeção de dependência
        private readonly IRepositorioAdesao repositorioAdesao;
        private readonly IRepositorioPeriodo repositorioPeriodo;
        #endregion

        #region Construtor
        public ServicoAdesao(IRepositorioAdesao repositorioAdesao, IRepositorioPeriodo repositorioPeriodo)
        {
            this.repositorioAdesao = repositorioAdesao;
            this.repositorioPeriodo = repositorioPeriodo;
        }
        #endregion

        #region IServicoAdesao
        public OperationResult RealizarAdesao(Participante participante, Periodo periodo, int numeroCotas)
        {
            var adesao = new Adesao { Participante = participante, Periodo = periodo, NumeroCotas = numeroCotas };
            
            repositorioAdesao.Incluir(adesao);

            if (repositorioAdesao.Commit() > 0) return OperationResult.OK;
            else return new OperationResult(StatusCodeEnum.Error, Mensagens.CommitError);
        }

        public Adesao ObterAdesao(int participanteId, int periodoId)
        {
            return repositorioAdesao.ObterAdesao(participanteId, periodoId);
        }

        public Adesao ObterAdesaoPorId(int id)
        {
            return repositorioAdesao.ObterPorId(id);
        }

        public Adesao ObterAdesaoAtiva(Participante participante)
        {
            var periodoAtivo = repositorioPeriodo.ObterPeriodoAtivo();

            if (periodoAtivo == null)
                return null;

            return ObterAdesao(participante.Id, periodoAtivo.Id);
        }

        public IReadOnlyCollection<Adesao> ListarAdesoesPeriodo(Periodo periodo)
        {
            return repositorioAdesao.ListarAdesoesPeriodo(periodo);
        }

        public int ObterTotalCotasPeriodo(Periodo periodo)
        {
            return repositorioAdesao.ObterTotalCotasPeriodo(periodo);
        }
        #endregion
    }
}
