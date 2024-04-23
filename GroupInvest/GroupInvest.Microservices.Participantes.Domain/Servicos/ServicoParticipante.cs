using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Participantes.Domain.Resources;
using GroupInvest.Microservices.Participantes.Domain.Validations.Participantes;
using System;
using System.Linq;
using System.Collections.Generic;

namespace GroupInvest.Microservices.Participantes.Domain.Servicos
{
    public class ServicoParticipante : IServicoParticipante
    {
        private readonly IRepositorioParticipante repositorioParticipante;
        private readonly IRepositorioAdesao repositorioAdesao;
        private readonly IRepositorioPeriodo repositorioPeriodo;

        #region Construtor
        public ServicoParticipante(
            IRepositorioParticipante repositorioParticipante,
            IRepositorioAdesao repositorioAdesao,
            IRepositorioPeriodo repositorioPeriodo)
        {
            this.repositorioParticipante = repositorioParticipante;
            this.repositorioAdesao = repositorioAdesao;
            this.repositorioPeriodo = repositorioPeriodo;
        }
        #endregion

        #region IParticipanteService
        public OperationResult IncluirParticipante(Participante participante)
        {
            var validation = new PersistenciaParticipanteValidation(repositorioParticipante);
            if (validation.IsValid(participante))
            {
                repositorioParticipante.Incluir(participante);

                if (repositorioParticipante.Commit() > 0) return OperationResult.OK;
                else return new OperationResult(StatusCodeEnum.Error, ResourceHelper.Get("CommitError"));
            }
            else
                return new OperationResult(StatusCodeEnum.Error, validation.Messages);
        }

        public OperationResult AlterarParticipante(Participante participante)
        {
            var validation = new PersistenciaParticipanteValidation(repositorioParticipante);
            if (validation.IsValid(participante))
            {
                repositorioParticipante.Alterar(participante);

                if (repositorioParticipante.Commit() > 0) return OperationResult.OK;
                else return new OperationResult(StatusCodeEnum.Error, ResourceHelper.Get("CommitError"));
            }
            else
                return new OperationResult(StatusCodeEnum.Error, validation.Messages);
        }

        public OperationResult InativarParticipante(Participante participante)
        {
            var validation = new InativacaoParticipanteValidation(repositorioPeriodo, repositorioAdesao);
            if (validation.IsValid(participante))
            {
                participante.Ativo = false;
                repositorioParticipante.Alterar(participante);

                if (repositorioParticipante.Commit() > 0) return OperationResult.OK;
                else return new OperationResult(StatusCodeEnum.Error, ResourceHelper.Get("CommitError"));
            }
            else
                return new OperationResult(StatusCodeEnum.Error, validation.Messages);
        }

        public OperationResult RealizarAdesaoParticipantePeriodoAtivo(Participante participante, int numeroCotas)
        {
            var periodoAtivo = repositorioPeriodo.ObterPeriodoAtivo();
            var adesao = new Adesao
            {
                DataAdesao = DateTime.Now,
                Participante = participante,
                Periodo = periodoAtivo,
                NumeroCotas = numeroCotas
            };

            var validation = new RealizarAdesaoValidation(repositorioAdesao);
            if (validation.IsValid(adesao))
            {
                repositorioAdesao.Incluir(adesao);
                if (repositorioAdesao.Commit() > 0) return OperationResult.OK;
                else return new OperationResult(StatusCodeEnum.Error, ResourceHelper.Get("CommitError"));
            }
            else
                return new OperationResult(StatusCodeEnum.Error, validation.Messages);
        }

        public OperationResult CancelarAdesaoParticipantePeriodoAtivo(Participante participante)
        {
            if (participante == null)
                return new OperationResult(StatusCodeEnum.Error, ResourceHelper.Get("ParticipanteNulo"));

            var adesaoAtiva = ObterAdesaoAtivaParticipante(participante);
            if (adesaoAtiva == null)
                return new OperationResult(StatusCodeEnum.Error, ResourceHelper.Get("AdesaoNaoEncontrada"));

            adesaoAtiva.DataCancelamento = DateTime.Now;

            var validation = new CancelarAdesaoValidation();
            if (validation.IsValid(adesaoAtiva))
            {
                repositorioAdesao.Alterar(adesaoAtiva);
                if (repositorioAdesao.Commit() > 0) return OperationResult.OK;
                else return new OperationResult(StatusCodeEnum.Error, ResourceHelper.Get("CommitError"));
            }
            else
                return new OperationResult(StatusCodeEnum.Error, validation.Messages);
        }

        public Participante ObterParticipantePorId(int id)
        {
            return repositorioParticipante.ObterPorId(id);
        }

        public Participante ObterParticipantePorEmail(string email)
        {
            return repositorioParticipante.ObterParticipantePorEmail(email);
        }

        public Participante ObterParticipantePorUsuarioAplicativo(string userId)
        {
            return repositorioParticipante.ObterParticipantePorUsuarioAplicativo(userId);
        }

        public Adesao ObterAdesaoAtivaParticipante(Participante participante)
        {
            var periodoAtivo = repositorioPeriodo.ObterPeriodoAtivo();
            if (periodoAtivo != null)
                return repositorioAdesao.ObterAdesao(participante, periodoAtivo);
            else
                return null;
        }

        public Adesao ObterAdesaoParticipantePeriodo(Participante participante, Periodo periodo)
        {
            return repositorioAdesao.ObterAdesao(participante, periodo);
        }

        public IReadOnlyCollection<Participante> ListarParticipantesAtivos()
        {
            var periodoAtivo = repositorioPeriodo.ObterPeriodoAtivo();
            var adesoes = repositorioAdesao.ListarAdesoesAtivasPeriodo(periodoAtivo);

            return adesoes.Select(ades => ades.Participante).ToList();
        }

        public IReadOnlyCollection<Participante> ListarParticipantes()
        {
            return repositorioParticipante.ListarTodos();
        }
        #endregion
    }
}
