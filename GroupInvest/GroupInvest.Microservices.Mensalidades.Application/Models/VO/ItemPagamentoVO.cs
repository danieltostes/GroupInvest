using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.VO
{
    public class ItemPagamentoVO
    {
        public int Id { get; set; }
        public MensalidadeVO Mensalidade { get; set; }
        public PagamentoEmprestimoVO Emprestimo { get; set; }
        public decimal Valor { get; set; }
    }
}
