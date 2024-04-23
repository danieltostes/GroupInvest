using GroupInvest.Common.Application.Events;
using GroupInvest.Microservices.AppUsuarios.Application.Models.VO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Events.Emprestimos
{
    public class IntegracaoEmprestimoAtualizacaoSaldoEvent : Event
    {
        public override string EventType => "IntegracaoEmprestimoAtualizacaoSaldo";

        public List<SaldoEmprestimoVO> Saldos { get; set; }
    }
}
