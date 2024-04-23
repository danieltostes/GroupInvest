using GroupInvest.Common.Application.Events;
using GroupInvest.Microservices.Mensalidades.Application.Models.VO;
using System.Collections.Generic;

namespace GroupInvest.Microservices.Mensalidades.Application.Events.Emprestimos
{
    public class PrevisoesPagamentoEmprestimosAtualizadasEvent : Event
    {
        public override string EventType => "PrevisoesPagamentoEmprestimosAtualizadas";
        public override string[] QueueNames => new string[] { "atualizacao-saldo-emprestimo-queue" };

        public List<SaldoEmprestimoVO> Saldos { get; set; }

        public PrevisoesPagamentoEmprestimosAtualizadasEvent()
        {
            Saldos = new List<SaldoEmprestimoVO>();
        }
    }
}
