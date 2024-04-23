using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Microservices.Auditoria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Auditoria.Domain.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para o repositório de auditoria
    /// </summary>
    public interface IRepositorioAuditoria : IRepositorio<int, AuditoriaBase>
    {
    }
}
