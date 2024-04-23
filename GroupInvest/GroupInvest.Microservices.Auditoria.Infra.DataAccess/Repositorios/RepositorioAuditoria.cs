using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.Auditoria.Domain.Entidades;
using GroupInvest.Microservices.Auditoria.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace GroupInvest.Microservices.Auditoria.Infra.DataAccess.Repositorios
{
    public class RepositorioAuditoria : EFRepositorio<int, AuditoriaBase>, IRepositorioAuditoria
    {
        public RepositorioAuditoria(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
