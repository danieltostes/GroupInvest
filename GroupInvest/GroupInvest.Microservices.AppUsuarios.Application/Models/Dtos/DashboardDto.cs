using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Models.Dtos
{
    public class DashboardDto
    {
        public decimal SaldoAcumulado { get; set; }
        public decimal RendimentoParcial { get; set; }
        public List<TransacaoDto> UltimasTransacoes { get; set; }
        public List<RendimentoParcialPeriodoDto> RendimentosParciais { get; set; }

        public DashboardDto()
        {
            UltimasTransacoes = new List<TransacaoDto>();
            RendimentosParciais = new List<RendimentoParcialPeriodoDto>();
        }
    }
}
