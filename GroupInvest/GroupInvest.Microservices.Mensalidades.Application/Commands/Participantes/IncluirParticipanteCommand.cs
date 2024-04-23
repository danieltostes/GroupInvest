namespace GroupInvest.Microservices.Mensalidades.Application.Commands.Participantes
{
    public class IncluirParticipanteCommand : ParticipanteCommand
    {
        #region Construtor
        public IncluirParticipanteCommand()
        {
            // construtor vazio para o auto mapper
        }

        public IncluirParticipanteCommand(int participanteId, string nome)
        {
            this.ParticipanteId = participanteId;
            this.Nome = nome;
        }
        #endregion
    }
}
