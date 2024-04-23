using GroupInvest.Common.Domain.Entidades;
using System;
using System.Collections.Generic;

namespace GroupInvest.Microservices.Mensalidades.Domain.Entidades
{
    public class Pagamento : Entidade<int>
    {
        public DateTime DataPagamento { get; set; }
        public decimal ValorTotalPagamento { get; set; }
        public List<ItemPagamento> ItensPagamento { get; set; }

        #region Construtor
        public Pagamento()
        {
            ItensPagamento = new List<ItemPagamento>();
        }
        #endregion
    }
}
