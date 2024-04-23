using GroupInvest.Common.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Entidades
{
    public class Periodo : Entidade<int>
    {
        public int AnoReferencia { get; set; }
        public DateTime? DataInicioPeriodo { get; set; }
        public DateTime? DataTerminoPeriodo { get; set; }
        public bool Ativo { get; set; }
    }
}
