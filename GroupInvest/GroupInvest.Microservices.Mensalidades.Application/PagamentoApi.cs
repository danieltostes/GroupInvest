using AutoMapper;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Pagamentos;
using GroupInvest.Microservices.Mensalidades.Application.Interfaces;
using GroupInvest.Microservices.Mensalidades.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application
{
    public class PagamentoApi : IPagamentoApi
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        #endregion

        #region Construtor
        public PagamentoApi(IMediatorHandler bus, IMapper mapper)
        {
            this.bus = bus;
        }
        #endregion

        #region IPagamentoApi
        public OperationResult RealizarPagamento(PagamentoDto pagamento)
        {
            var command = new RealizarPagamentoCommand(pagamento);
            return bus.SendCommand(command);
        }

        public OperationResult RealizarPagamentoRetroativo(PagamentoDto pagamento)
        {
            var command = new RealizarPagamentoRetroativoCommand(pagamento);
            return bus.SendCommand(command);
        }
        #endregion
    }
}
