using GroupInvest.Common.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Entidades
{
    public class ItemPagamento : Entidade<int>
    {
        public Pagamento Pagamento { get; set; }
        public Mensalidade Mensalidade { get; set; }
        public PrevisaoPagamentoEmprestimo PrevisaoPagamentoEmprestimo { get; set; }
        public decimal Valor { get; set; }
    }
}
