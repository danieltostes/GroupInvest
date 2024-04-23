using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using GroupInvest.Common.Domain.Results;

namespace GroupInvest.Microservices.AppUsuarios.Infra.DataAccess.Repositorios
{
    public class RepositorioMensalidade : MongoRepositorio<Mensalidade>, IRepositorioMensalidade
    {
        protected override string CollectionName => "Mensalidades";

        #region Construtor
        public RepositorioMensalidade(IMongoDatabase db)
            : base(db)
        {
        }
        #endregion

        #region MongoRepositorio
        public override void Alterar(Mensalidade entidade)
        {
            var filter = Builders<Mensalidade>.Filter.Eq(mens => mens.MensalidadeId, entidade.MensalidadeId);
            db.GetCollection<Mensalidade>(CollectionName).ReplaceOne(filter, entidade);
        }
        #endregion

        #region IRepositorioMensalidade
        public void IncluirPagamento(Mensalidade mensalidade, PagamentoMensalidade pagamento)
        {
            var filter = Builders<Mensalidade>.Filter.Eq(mens => mens.MensalidadeId, mensalidade.MensalidadeId);
            var update = Builders<Mensalidade>.Update.Set(mens => mens.Pagamento, pagamento);
            db.GetCollection<Mensalidade>(CollectionName).UpdateOne(filter, update);
        }

        public IReadOnlyCollection<Mensalidade> ListarMensalidadesParticipante(int participanteId)
        {
            var filter = Builders<Mensalidade>.Filter.Eq(mens => mens.ParticipanteId, participanteId);
            return db.GetCollection<Mensalidade>(CollectionName).Find(filter).ToList();
        }

        public Mensalidade ObterMensalidadePorId(int id)
        {
            var filter = Builders<Mensalidade>.Filter.Eq(mens => mens.MensalidadeId, id);
            return db.GetCollection<Mensalidade>(CollectionName).Find(filter).FirstOrDefault();
        }

        public IReadOnlyCollection<Mensalidade> ListarMensalidadesPagasParticipante(int participanteId)
        {
            var filter = Builders<Mensalidade>.Filter.And(new FilterDefinition<Mensalidade>[]
            {
                Builders<Mensalidade>.Filter.Eq(mens => mens.ParticipanteId, participanteId),
                Builders<Mensalidade>.Filter.Ne(mens => mens.Pagamento, null),
            });

            var mensalidades = db.GetCollection<Mensalidade>(CollectionName).Find(filter).ToList();
            return mensalidades;
        }
        #endregion
    }
}
