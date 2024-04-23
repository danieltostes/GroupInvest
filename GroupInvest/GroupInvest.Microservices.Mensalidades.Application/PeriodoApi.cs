using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Periodos;
using GroupInvest.Microservices.Mensalidades.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application
{
    public class PeriodoApi : IPeriodoApi
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        #endregion

        #region Construtor
        public PeriodoApi(IMediatorHandler bus)
        {
            this.bus = bus;
        }
        #endregion

        #region IPeriodoApi
        public OperationResult EncerrarPeriodo(int id)
        {
            var command = new EncerrarPeriodoCommand(id);
            return bus.SendCommand(command);
        }

        public OperationResult CalcularRendimentoParcialPeriodo(DateTime dataReferencia)
        {
            var command = new CalcularRendimentoParcialPeriodoCommand(dataReferencia);
            return bus.SendCommand(command);
        }
        #endregion
    }
}
