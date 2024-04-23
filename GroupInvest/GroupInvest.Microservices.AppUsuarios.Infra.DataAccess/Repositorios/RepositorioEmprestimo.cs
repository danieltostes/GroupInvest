using GroupInvest.Common.Infrastructure.DataAccess.Repositorios;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Infra.DataAccess.Repositorios
{
    public class RepositorioEmprestimo : MongoRepositorio<Emprestimo>, IRepositorioEmprestimo
    {
        protected override string CollectionName => "Emprestimos";

        #region Construtor
        public RepositorioEmprestimo(IMongoDatabase db):
            base(db)
        {
        }
        #endregion

        #region IMongoRepositorio
        public override void Alterar(Emprestimo entidade)
        {
            var filter = Builders<Emprestimo>.Filter.Eq(emp => emp.EmprestimoId, entidade.EmprestimoId);
            db.GetCollection<Emprestimo>(CollectionName).ReplaceOne(filter, entidade);
        }
        #endregion

        #region IRepositorioEmprestimo
        public void AdicionarPagamento(Emprestimo emprestimo, PagamentoEmprestimo pagamento)
        {
            var filter = Builders<Emprestimo>.Filter.Eq(emp => emp.EmprestimoId, emprestimo.EmprestimoId);
            var update = Builders<Emprestimo>.Update.Push(emp => emp.Pagamentos, pagamento);
            db.GetCollection<Emprestimo>(CollectionName).FindOneAndUpdate(filter, update);
        }

        public void AtualizarSaldoEmprestimo(Emprestimo emprestimo)
        {
            var filter = Builders<Emprestimo>.Filter.Eq(emp => emp.EmprestimoId, emprestimo.EmprestimoId);
            var update = Builders<Emprestimo>.Update.Set(emp => emp.Saldo, emprestimo.Saldo);
            db.GetCollection<Emprestimo>(CollectionName).FindOneAndUpdate(filter, update);
        }

        public void AtualizarProximoPagamentoEmprestimo(Emprestimo emprestimo)
        {
            //var filter = Builders<Emprestimo>.Filter.Eq(emp => emp.EmprestimoId, emprestimo.EmprestimoId);
            //var update = Builders<Emprestimo>.Update.Set(emp => emp.ProximoPagamento, emprestimo.ProximoPagamento);
            //db.GetCollection<Emprestimo>(CollectionName).FindOneAndUpdate(filter, update);
        }

        public IReadOnlyCollection<Emprestimo> ListarEmprestimosParticipante(int participanteId)
        {
            var filter = Builders<Emprestimo>.Filter.Eq(emp => emp.ParticipanteId, participanteId);
            return db.GetCollection<Emprestimo>(CollectionName).Find(filter).ToList();
        }

        public Emprestimo ObterEmprestimoPorId(int id)
        {
            var filter = Builders<Emprestimo>.Filter.Eq(emp => emp.EmprestimoId, id);
            return db.GetCollection<Emprestimo>(CollectionName).Find(filter).FirstOrDefault();
        }
        #endregion
    }
}
