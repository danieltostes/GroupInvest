using GroupInvest.Common.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Events.Emprestimos
{
    public class EmprestimoConcedidoRegistradoEvent : Event
    {
        public override string EventType => "EmprestimoConcedidoRegistrado";
    }
}
