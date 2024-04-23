using GroupInvest.Common.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Common.Domain.Interfaces
{
    /// <summary>
    /// Interface para os repositórios MongoDB
    /// </summary>
    public interface IMongoRepositorio<T>
    {
        /// <summary>
        /// Inclui uma entidade no repositório
        /// </summary>
        /// <param name="entidade"></param>
        void Incluir(T entidade);

        /// <summary>
        /// Altera uma entidade no repositório
        /// </summary>
        /// <param name="entidade">Entidade</param>
        void Alterar(T entidade);
    }
}
