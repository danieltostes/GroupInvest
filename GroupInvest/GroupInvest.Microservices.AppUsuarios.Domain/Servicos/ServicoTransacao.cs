using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Servicos
{
    public class ServicoTransacao : IServicoTransacao
    {
        #region Injeção de dependência
        private IRepositorioTransacao repositorioTransacao;
        #endregion

        #region Construtor
        public ServicoTransacao(IRepositorioTransacao repositorioTransacao)
        {
            this.repositorioTransacao = repositorioTransacao;
        }
        #endregion

        #region IServicoTransacao
        public OperationResult IncluirTransacao(Transacao transacao)
        {
            try { repositorioTransacao.Incluir(transacao); }
            catch (Exception ex) { return new OperationResult(StatusCodeEnum.Error, ex.Message); }

            return OperationResult.OK;
        }

        public IReadOnlyCollection<Transacao> ListarUltimasTransacoesParticipante(int participanteId)
        {
            return repositorioTransacao.ListarUltimasTransacoesParticipante(participanteId);
        }
        #endregion
    }
}
