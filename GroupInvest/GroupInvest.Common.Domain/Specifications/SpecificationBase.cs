using GroupInvest.Common.Domain.Interfaces;
using System.Collections.Generic;

namespace GroupInvest.Common.Domain.Specifications
{
    /// <summary>
    /// Classe base para specifications
    /// </summary>
    /// <typeparam name="T">Tipo da entidade</typeparam>
    public abstract class SpecificationBase<T> : ISpecification<T>
    {
        private List<string> messages;
        public IReadOnlyCollection<string> Messages { get => messages; }

        #region Construtor
        public SpecificationBase()
        {
            messages = new List<string>();
        }
        #endregion

        #region Métodos
        protected void AddMessage(string message)
        {
            messages.Add(message);
        }
        
        public abstract bool IsSatisfiedBy(T entidade);
        #endregion

    }
}
