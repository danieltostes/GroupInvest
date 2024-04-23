using GroupInvest.Common.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Events.Periodos
{
    public class IntegracaoRendimentoParcialPeriodoEvent : Event
    {
        public override string EventType => "IntegracaoRendimentoParcialPeriodo";
        public override string[] QueueNames => new string[] { "atualizacao-rendimento-parcial-queue" };

        public DateTime DataReferencia { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public decimal PercentualRendimento { get; set; }
    }
}
