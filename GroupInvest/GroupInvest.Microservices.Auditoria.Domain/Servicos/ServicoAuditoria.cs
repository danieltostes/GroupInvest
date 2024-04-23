using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Auditoria.Domain.Entidades;
using GroupInvest.Microservices.Auditoria.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Auditoria.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Auditoria.Domain.Servicos
{
    public class ServicoAuditoria : IServicoAuditoria
    {
        private readonly IRepositorioAuditoria repositorioAuditoria;

        #region Construtor
        public ServicoAuditoria(IRepositorioAuditoria repositorioAuditoria)
        {
            this.repositorioAuditoria = repositorioAuditoria;
        }
        #endregion

        #region IServicoAuditoria
        public OperationResult IncluirAuditoria(AuditoriaBase auditoria)
        {
            repositorioAuditoria.Incluir(auditoria);
            if (repositorioAuditoria.Commit() > 0) return OperationResult.OK;
            else return new OperationResult(StatusCodeEnum.Error, "Commit não efetuou alterações na base de dados.");
        }
        #endregion
    }
}
