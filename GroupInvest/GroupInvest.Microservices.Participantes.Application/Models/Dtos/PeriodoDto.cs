using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Models.Dtos
{
    public class PeriodoDto
    {
        public int Id { get; set; }
        public int AnoReferencia { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public bool Ativo { get; set; }
    }
}
