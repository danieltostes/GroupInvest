using GroupInvest.Common.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Events
{
    public class TransacaoRealizadaEvent : Event
    {
        public override string EventType => "TransacaoRealizada";
        public override string[] QueueNames => new string[] { "transacoes-queue" };

        public int ParticipanteId { get; set; }
        public int CodigoTransacao { get; set; }
        public DateTime DataTransacao { get; set; }
        public string Descricao { get; set; }
        public decimal ValorTransacao { get; set; }
    }
}
