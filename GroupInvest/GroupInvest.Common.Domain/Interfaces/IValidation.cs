using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Common.Domain.Interfaces
{
    /// <summary>
    /// Interface para validações
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidation<T>
    {
        bool IsValid(T entidade);
    }
}
