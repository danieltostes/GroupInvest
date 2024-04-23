using GroupInvest.Common.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Domain.Entidades
{
    public class Periodo : Entidade<int>
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public int AnoReferencia { get; set; }
        public bool Ativo { get; set; }
    }
}
