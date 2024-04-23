using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupInvest.Microservices.Participantes.Infra.DataAccess.Repositorios
{
    public class RepositorioParticipante : EFRepositorio<int, Participante>, IRepositorioParticipante
    {
        #region Construtor
        public RepositorioParticipante(DbContext dbContext): base(dbContext)
        {
        }
        #endregion

        #region IRepositorioParticipante
        public Participante ObterParticipantePorEmail(string email)
        {
            if (!db.Set<Participante>().Any())
                return null;

            return db.Set<Participante>().FirstOrDefault(p => p.Email.Equals(email));
        }

        public Participante ObterParticipantePorUsuarioAplicativo(string userId)
        {
            if (!db.Set<Participante>().Any())
                return null;

            if (Guid.TryParse(userId, out var userGuid))
                return db.Set<Participante>().FirstOrDefault(p => p.UsuarioAplicativoId.Equals(userGuid));
            else
                return null;
        }

        public IReadOnlyCollection<Participante> ListarTodos()
        {
            if (!db.Set<Participante>().Any())
                return new List<Participante>();

            return db.Set<Participante>().ToList();
        }
        #endregion
    }
}
