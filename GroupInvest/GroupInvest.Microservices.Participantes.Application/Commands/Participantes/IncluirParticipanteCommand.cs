namespace GroupInvest.Microservices.Participantes.Application.Commands.Participantes
{
    public class IncluirParticipanteCommand : ParticipanteCommand
    {
        #region Construtor
        public IncluirParticipanteCommand(string nome, string email, string telefone)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
        }
        #endregion
    }
}
