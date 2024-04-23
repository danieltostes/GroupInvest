using GroupInvest.Common.Application.Commands;

namespace GroupInvest.Microservices.Mensalidades.Application.Commands.Adesoes
{
    public class RealizarAdesaoCommand : Command
    {
        public int ParticipanteId { get; set; }
        public int PeriodoId { get; set; }
        public int NumeroCotas { get; set; }

        public RealizarAdesaoCommand(int participanteId, int periodoId, int numeroCotas)
        {
            ParticipanteId = participanteId;
            PeriodoId = periodoId;
            NumeroCotas = numeroCotas;
        }
    }
}
