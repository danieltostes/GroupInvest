using GroupInvest.Common.Application.Events;
using GroupInvest.Microservices.AppUsuarios.Application.Models.VO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Events.Emprestimos
{
    public class IntegracaoEmprestimoConcedidoEvent : Event
    {
        public override string EventType => "IntegracaoEmprestimoConcedido";

        public EmprestimoVO Emprestimo { get; set; }
    }
}
