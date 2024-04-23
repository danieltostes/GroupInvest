using GroupInvest.Common.Application.Commands;

namespace GroupInvest.Microservices.Mensalidades.Application.Commands.Participantes
{
    public abstract class ParticipanteCommand : Command
    {
        public int ParticipanteId { get; set; }
        public string Nome { get; set; }
    }
}
