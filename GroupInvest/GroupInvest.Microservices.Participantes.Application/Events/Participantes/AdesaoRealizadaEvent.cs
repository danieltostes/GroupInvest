using GroupInvest.Common.Application.Events;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Events.Participantes
{
    public class AdesaoRealizadaEvent : Event
    {
        public override string[] QueueNames => new string[] { "adesoes-queue" };
        public override string EventType => "AdesaoRealizada";

        public Participante Participante { get; set; }
        public Periodo Periodo { get; set; }
        public int NumeroCotas { get; set; }
        public DateTime DataAdesao { get; set; }
    }
}
