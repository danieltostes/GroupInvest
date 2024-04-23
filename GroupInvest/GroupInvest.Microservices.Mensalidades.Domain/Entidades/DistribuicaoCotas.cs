using GroupInvest.Common.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Entidades
{
    public class DistribuicaoCotas : Entidade<int>
    {
        public Periodo Periodo { get; set; }
        public decimal ValorPrevisto { get; set; }
        public decimal ValorArrecadado { get; set; }
        public int NumeroTotalCotas { get; set; }
        public decimal PercentualRendimento { get; set; }

        public List<DistribuicaoParticipante> DistribuicaoParticipantes { get; set; }

        public DistribuicaoCotas()
        {
            DistribuicaoParticipantes = new List<DistribuicaoParticipante>();
        }
    }
}
