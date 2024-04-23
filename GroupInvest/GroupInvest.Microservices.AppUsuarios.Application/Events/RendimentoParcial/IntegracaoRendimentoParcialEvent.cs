using GroupInvest.Common.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Events.RendimentoParcial
{
    public class IntegracaoRendimentoParcialEvent : Event
    {
        public override string EventType => "IntegracaoRendimentoParcial";

        public DateTime DataReferencia { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public decimal PercentualRendimento { get; set; }
    }
}
