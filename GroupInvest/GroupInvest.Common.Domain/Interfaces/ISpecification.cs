using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Common.Domain.Interfaces
{
    /// <summary>
    /// Interface para specifications
    /// </summary>
    /// <typeparam name="T">Tipo da entidade</typeparam>
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entidade);
        IReadOnlyCollection<string> Messages { get; }
    }
}
