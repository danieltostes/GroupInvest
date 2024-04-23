using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.VO
{
    public class EmprestimoVO
    {
        public int Id { get; set; }
        public int ParticipanteId { get; set; }
        public DateTime Data { get; set; }
        public DateTime? DataProximoVencimento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public bool Quitado { get; set; }
    }
}
