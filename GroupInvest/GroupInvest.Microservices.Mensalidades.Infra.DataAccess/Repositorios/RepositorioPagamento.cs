using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Repositorios
{
    public class RepositorioPagamento : EFRepositorio<int, Pagamento>, IRepositorioPagamento
    {
        #region Construtor
        public RepositorioPagamento(DbContext context) : base(context)
        {
        }
        #endregion
    }
}
