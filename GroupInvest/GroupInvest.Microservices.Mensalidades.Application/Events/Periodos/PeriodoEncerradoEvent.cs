using GroupInvest.Common.Application.Events;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Events.Periodos
{
    public class PeriodoEncerradoEvent : Event
    {
        public override string EventType => "PeriodoEncerrado";

        public DistribuicaoCotas DistribuicaoCotas { get; set; }
    }
}
