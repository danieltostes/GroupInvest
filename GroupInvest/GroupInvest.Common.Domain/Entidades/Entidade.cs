using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Common.Domain.Entidades
{
    // Classe base para as entidades do domínio
    public abstract class Entidade<TKey>
    {
        public TKey Id { get; set; }
    }
}
