using System;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Dtos
{
    public class ConcessaoEmprestimoDto
    {
        public int ParticipanteId { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public decimal ValorEmprestimo { get; set; }
    }
}
