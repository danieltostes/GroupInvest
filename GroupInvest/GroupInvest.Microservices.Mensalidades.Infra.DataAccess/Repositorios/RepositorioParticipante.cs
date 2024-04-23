using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Repositorios
{
    public class RepositorioParticipante : EFRepositorio<int, Participante>, IRepositorioParticipante
    {
        #region Construtor
        public RepositorioParticipante(DbContext context) : base(context)
        {
        }
        #endregion
    }
}
