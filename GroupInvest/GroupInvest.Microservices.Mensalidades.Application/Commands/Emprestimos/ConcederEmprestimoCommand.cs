using GroupInvest.Common.Application.Commands;
using System;

namespace GroupInvest.Microservices.Mensalidades.Application.Commands.Emprestimos
{
    public class ConcederEmprestimoCommand : Command
    {
        public int ParticipanteId { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public decimal ValorEmprestimo { get; set; }

        public ConcederEmprestimoCommand(int participanteId, DateTime dataEmprestimo, decimal valorEmprestimo)
        {
            this.ParticipanteId = participanteId;
            this.DataEmprestimo = dataEmprestimo;
            this.ValorEmprestimo = valorEmprestimo;
        }
    }
}
