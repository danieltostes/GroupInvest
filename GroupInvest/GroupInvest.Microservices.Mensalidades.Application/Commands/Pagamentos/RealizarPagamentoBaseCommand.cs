using GroupInvest.Common.Application.Commands;
using GroupInvest.Microservices.Mensalidades.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Commands.Pagamentos
{
    public class RealizarPagamentoBaseCommand : Command
    {
        public PagamentoDto PagamentoDto { get; set; }

        public RealizarPagamentoBaseCommand(PagamentoDto dto)
        {
            PagamentoDto = dto;
        }
    }
}
