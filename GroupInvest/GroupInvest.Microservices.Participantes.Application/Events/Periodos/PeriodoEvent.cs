using GroupInvest.Common.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Events.Periodos
{
    public abstract class PeriodoEvent : Event
    {
        public int AnoReferencia { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public bool Ativo { get; set; }
    }
}
