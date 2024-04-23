using GroupInvest.Common.Domain.Entidades;
using GroupInvest.Common.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroupInvest.Common.Infrastructure.DataAccess.Repositorios
{
    public abstract class EFRepositorio<TKey, T> : IRepositorio<TKey, T> where T : Entidade<TKey>
    {
        protected DbContext db;

        #region Construtor
        protected EFRepositorio(DbContext dbContext)
        {
            db = dbContext;
        }
        #endregion

        #region IRepositorio
        /// <summary>
        /// Inclui uma entidade no repositório
        /// </summary>
        /// <param name="entidade">Entidade</param>
        public void Incluir(T entidade)
        {
            db.Set<T>().Add(entidade);
        }

        /// <summary>
        /// Altera uma entidade no repositório
        /// </summary>
        /// <param name="entidade">Entidade</param>
        public void Alterar(T entidade)
        {
            db.Set<T>().Update(entidade);
        }

        /// <summary>
        /// Remove uma  entidade do repositório
        /// </summary>
        /// <param name="entidade">Entidade</param>
        public void Excluir(T entidade)
        {
            db.Set<T>().Remove(entidade);
        }

        /// <summary>
        /// Obtém uma entidade no repositório através do seu identificador
        /// </summary>
        /// <param name="id">Identificador da entidade</param>
        public virtual T ObterPorId(TKey id)
        {
            return db.Set<T>().Find(id);
        }

        /// <summary>
        /// Confirma as alterações para a base de dados
        /// </summary>
        /// <returns>Número de alterações na base de dados</returns>
        public int Commit()
        {
            return db.SaveChanges();
        }
        #endregion
    }
}
