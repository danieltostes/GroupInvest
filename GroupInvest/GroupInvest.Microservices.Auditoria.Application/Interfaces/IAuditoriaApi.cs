using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Auditoria.Application.Models.Dtos;
using GroupInvest.Microservices.Auditoria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.Auditoria.Application.Interfaces
{
    public interface IAuditoriaApi
    {
        OperationResult IncluirAuditoria(AuditoriaDto dto);
    }
}
