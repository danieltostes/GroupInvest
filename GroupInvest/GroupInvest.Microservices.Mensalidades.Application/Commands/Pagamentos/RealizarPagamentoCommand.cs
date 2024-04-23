using GroupInvest.Common.Application.Commands;
using GroupInvest.Microservices.Mensalidades.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Commands.Pagamentos
{
    public class RealizarPagamentoCommand : RealizarPagamentoBaseCommand
    {
        public RealizarPagamentoCommand(PagamentoDto dto) :
            base(dto)
        {
        }
    }
}
