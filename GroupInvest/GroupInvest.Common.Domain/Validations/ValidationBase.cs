using GroupInvest.Common.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Common.Domain.Validations
{
    /// <summary>
    /// Classe base para validations
    /// </summary>
    /// <typeparam name="T">Tipo de entidade</typeparam>
    public abstract class ValidationBase<T> : IValidation<T>
    {
        #region Propriedades
        protected List<string> messages;
        protected List<ISpecification<T>> specifications;

        public IReadOnlyCollection<string> Messages { get => messages; }
        #endregion

        #region Construtor
        protected ValidationBase()
        {
            messages = new List<string>();
            specifications = new List<ISpecification<T>>();
        }
        #endregion

        #region Métodos
        public bool IsValid(T entidade)
        {
            foreach (var specification in specifications)
            {
                if (!specification.IsSatisfiedBy(entidade))
                {
                    messages.AddRange(specification.Messages);
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
