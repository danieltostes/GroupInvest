using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Infra.DataAccess.Repositorios
{
    public class RepositorioRendimentoParcialPeriodo : MongoRepositorio<RendimentoParcialPeriodo>, IRepositorioRendimentoParcialPeriodo
    {
        protected override string CollectionName => "RendimentosParciais";

        #region Construtor
        public RepositorioRendimentoParcialPeriodo(IMongoDatabase db) : base(db)
        {
        }
        #endregion

        #region Overrides
        public override void Alterar(RendimentoParcialPeriodo entidade)
        {
            var filter = Builders<RendimentoParcialPeriodo>.Filter.Eq(rend => rend.DataReferencia, entidade.DataReferencia);
            db.GetCollection<RendimentoParcialPeriodo>(CollectionName).ReplaceOne(filter, entidade);
        }
        #endregion

        #region IRepositorioRendimentoParcialPeriodo
        public RendimentoParcialPeriodo ObterRendimentoParcialPeriodoDataReferencia(DateTime dataReferencia)
        {
            var filter = Builders<RendimentoParcialPeriodo>.Filter.Eq(rend => rend.DataReferencia, dataReferencia);
            return db.GetCollection<RendimentoParcialPeriodo>(CollectionName).Find(filter).FirstOrDefault();
        }

        public IReadOnlyCollection<RendimentoParcialPeriodo> ListarRendimentosParciais()
        {
            return db.GetCollection<RendimentoParcialPeriodo>(CollectionName).Find(Builders<RendimentoParcialPeriodo>.Filter.Empty).ToList();
        }
        #endregion
    }
}
