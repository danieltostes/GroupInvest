using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Auditoria.Application.Interfaces;
using GroupInvest.Microservices.Auditoria.Application.Models.Dtos;
using System;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.Auditoria.Application
{
    public class AuditoriaAPI : IAuditoriaApi
    {
        public OperationResult IncluirAuditoria(AuditoriaDto dto)
        {
            return OperationResult.OK;
        }
    }
}
