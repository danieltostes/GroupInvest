using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Infra.DataAccess.Repositorios
{
    public class RepositorioTransacao : MongoRepositorio<Transacao>, IRepositorioTransacao
    {
        protected override string CollectionName => "Transacoes";

        #region Construtor
        public RepositorioTransacao(IMongoDatabase db) : base(db)
        {
        }
        #endregion

        #region Overrides
        public override void Alterar(Transacao entidade)
        {
        }
        #endregion

        #region IRepositorioTransacao
        public IReadOnlyCollection<Transacao> ListarUltimasTransacoesParticipante(int participanteId)
        {
            var filter = Builders<Transacao>.Filter.Eq(trans => trans.ParticipanteId, participanteId);
            return db.GetCollection<Transacao>(CollectionName).Find(filter).ToList();
        }
        #endregion
    }
}
