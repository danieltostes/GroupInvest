using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Dtos
{
    public class EmprestimoDto
    {
        public int Id { get; set; }
        public ParticipanteDto Participante { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataProximoPagamento { get; set; }
        public decimal ValorEmprestimo { get; set; }
        public decimal SaldoEmprestimo { get; set; }
        public string StatusEmprestimo { get; set; }
    }
}
