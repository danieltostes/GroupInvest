using GroupInvest.Common.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Events.Mensalidades
{
    public class MensalidadesRegistradasEvent : Event
    {
        public override string EventType => "MensalidadesRegistradas";
    }
}
