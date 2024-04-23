using GroupInvest.Common.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Entidades
{
    public class DistribuicaoParticipante : Entidade<int>
    {
        public DistribuicaoCotas DistribuicaoCotas { get; set; }
        public Participante Participante { get; set; }
        public decimal ValorDistribuicao { get; set; }
        public decimal SaldoDevedor { get; set; }
        public decimal ValorTotalReceber { get; set; }
    }
}
