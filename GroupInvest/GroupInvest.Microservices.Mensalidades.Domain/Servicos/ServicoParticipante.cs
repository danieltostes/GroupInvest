using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Servicos
{
    public class ServicoParticipante : IServicoParticipante
    {
        private readonly IRepositorioParticipante repositorioParticipante;

        #region Construtor
        public ServicoParticipante(IRepositorioParticipante repositorioParticipante)
        {
            this.repositorioParticipante = repositorioParticipante;
        }
        #endregion

        #region IServicoParticipante
        public OperationResult IncluirParticipante(Participante participante)
        {
            repositorioParticipante.Incluir(participante);
            if (repositorioParticipante.Commit() > 0) return OperationResult.OK;
            else return new OperationResult(StatusCodeEnum.Error, "Commit não efetuou alterações na base de dados");
        }

        public OperationResult ExcluirParticipante(Participante participante)
        {
            return OperationResult.OK;
        }

        public Participante ObterParticipantePorId(int id)
        {
            return repositorioParticipante.ObterPorId(id);
        }
        #endregion
    }
}
