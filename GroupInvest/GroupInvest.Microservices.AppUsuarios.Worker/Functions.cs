using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Emprestimos;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Mensalidades;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Pagamentos;
using GroupInvest.Microservices.AppUsuarios.Application.Events.RendimentoParcial;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Transacoes;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Worker
{
    public class Functions
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        #endregion

        #region Construtor
        public Functions(IMediatorHandler bus)
        {
            this.bus = bus;
        }
        #endregion

        #region Processamento das Filas
        public void ProcessarMensalidadesQueue([ServiceBusTrigger("mensalidades-queue")] string message, ILogger logger)
        {
            var evento = JsonConvert.DeserializeObject<IntegracaoMensalidadesGeradasEvent>(message);
            bus.PublishEvent(evento);
        }

        public void ProcessarEmprestimosQueue([ServiceBusTrigger("emprestimos-queue")] string message, ILogger logger)
        {
            var evento = JsonConvert.DeserializeObject<IntegracaoEmprestimoConcedidoEvent>(message);
            bus.PublishEvent(evento);
        }

        public void ProcessarAtualizacaoSaldosEmprestimosQueue([ServiceBusTrigger("atualizacao-saldo-emprestimo-queue")] string message, ILogger logger)
        {
            var evento = JsonConvert.DeserializeObject<IntegracaoEmprestimoAtualizacaoSaldoEvent>(message);
            bus.PublishEvent(evento);
        }

        public void ProcessarPagamentosQueue([ServiceBusTrigger("pagamentos-queue")] string message, ILogger logger)
        {
            var evento = JsonConvert.DeserializeObject<IntegracaoPagamentoRealizadoEvent>(message);
            bus.PublishEvent(evento);
        }

        public void ProcessarTransacoesQueue([ServiceBusTrigger("transacoes-queue")] string message, ILogger logger)
        {
            var evento = JsonConvert.DeserializeObject<IntegracaoTransacaoRealizadaEvent>(message);
            bus.PublishEvent(evento);
        }

        public void ProcessarAtualizacaoRendimentoParcialQueue([ServiceBusTrigger("atualizacao-rendimento-parcial-queue")] string message, ILogger logger)
        {
            var evento = JsonConvert.DeserializeObject<IntegracaoRendimentoParcialEvent>(message);
            bus.PublishEvent(evento);
        }
        #endregion
    }
}
