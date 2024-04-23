using GroupInvest.Common.Application.Events;
using GroupInvest.Microservices.Mensalidades.Application.Models.VO;

namespace GroupInvest.Microservices.Mensalidades.Application.Events.Emprestimos
{
    public class EmprestimoConcedidoEvent : Event
    {
        public override string EventType => "EmprestimoConcedido";
        public override string[] QueueNames => new string[] { "emprestimos-queue" };

        public EmprestimoVO Emprestimo { get; set; }
    }
}
