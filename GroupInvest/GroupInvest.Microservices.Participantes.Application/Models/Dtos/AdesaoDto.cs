using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Models.Dtos
{
    public class AdesaoDto
    {
        public int ParticipanteId { get; set; }
        public int PeriodoId { get; set; }
        public int NumeroCotas { get; set; }
        public DateTime DataAdesao { get; internal set; }
        public DateTime? DataCancelamento { get; set; }
    }
}
