using GroupInvest.Common.Application.Commands;
using GroupInvest.Microservices.AppUsuarios.Application.Models.VO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Commands.Pagamentos
{
    public class RegistrarPagamentoCommand : Command
    {
        public int Id { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorTotalPagamento { get; set; }

        public List<ItemPagamentoVO> ItensPagamento { get; set; }
    }
}
