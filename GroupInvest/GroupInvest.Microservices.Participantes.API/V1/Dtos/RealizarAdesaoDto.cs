using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.Participantes.API.V1.Dtos
{
    public class RealizarAdesaoDto
    {
        public int ParticipanteId { get; set; }
        public int NumeroCotas { get; set; }
    }
}
