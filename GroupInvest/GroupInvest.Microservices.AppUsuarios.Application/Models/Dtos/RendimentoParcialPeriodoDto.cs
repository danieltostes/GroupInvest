using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Models.Dtos
{
    public class RendimentoParcialPeriodoDto
    {
        public DateTime DataReferencia { get; set; }
        public decimal PercentualRendimento { get; set; }
    }
}
