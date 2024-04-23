using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Auditoria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Auditoria.Domain.Interfaces.Servicos
{
    /// <summary>
    /// Interface para o serviço de auditoria
    /// </summary>
    public interface IServicoAuditoria
    {
        /// <summary>
        /// Inclui um registro de auditoria
        /// </summary>
        /// <param name="auditoria">Registro de auditoria</param>
        OperationResult IncluirAuditoria(AuditoriaBase auditoria);
    }
}
