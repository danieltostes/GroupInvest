using GroupInvest.Common.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Events.RendimentoParcial
{
    public class RendimentoParcialRegistradoEvent : Event
    {
        public override string EventType => "RendimentoParcialRegistrado";
    }
}
