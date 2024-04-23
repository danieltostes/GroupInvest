using GroupInvest.Common.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroupInvest.Common.Domain.Interfaces
{
    // Interface para os repositórios
    public interface IRepositorio<TKey, T> where T : Entidade<TKey>
    {
        /// <summary>
        /// Inclui uma entidade no repositório
        /// </summary>
        /// <param name="entidade">Entidade</param>
        void Incluir(T entidade);

        /// <summary>
        /// Altera uma entidade no repositório
        /// </summary>
        /// <param name="entidade">Entidade</param>
        void Alterar(T entidade);

        /// <summary>
        /// Remove uma  entidade do repositório
        /// </summary>
        /// <param name="entidade">Entidade</param>
        void Excluir(T entidade);

        /// <summary>
        /// Obtém uma entidade no repositório através do seu identificador
        /// </summary>
        /// <param name="id">Identificador da entidade</param>
        /// <returns>Entidade</returns>
        T ObterPorId(TKey id);

        /// <summary>
        /// Confirma as alterações para a base de dados
        /// </summary>
        /// <returns>Número de alterações na base de dados</returns>
        int Commit();
    }
}
