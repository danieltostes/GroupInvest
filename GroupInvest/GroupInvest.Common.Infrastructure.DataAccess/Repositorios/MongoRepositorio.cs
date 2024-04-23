using GroupInvest.Common.Domain.Interfaces;
using MongoDB.Driver;

namespace GroupInvest.Common.Infrastructure.DataAccess.Repositorios
{
    public abstract class MongoRepositorio<T> : IMongoRepositorio<T>
    {
        protected IMongoDatabase db;
        protected abstract string CollectionName { get; }

        #region Construtor
        public MongoRepositorio(IMongoDatabase db)
        {
            this.db = db;
        }
        #endregion

        #region IMongoRepositorio
        public void Incluir(T entidade)
        {
            var collection = db.GetCollection<T>(CollectionName);
            collection.InsertOne(entidade);
        }

        public abstract void Alterar(T entidade);
        #endregion
    }
}
