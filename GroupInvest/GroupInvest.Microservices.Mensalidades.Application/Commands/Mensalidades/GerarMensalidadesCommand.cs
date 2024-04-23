using GroupInvest.Common.Application.Commands;

namespace GroupInvest.Microservices.Mensalidades.Application.Commands.Mensalidades
{
    public class GerarMensalidadesCommand : Command
    {
        public int AdesaoId { get; set; }

        #region Construtor
        public GerarMensalidadesCommand()
        {
            // construtor vazio para o auto mapper
        }

        public GerarMensalidadesCommand(int adesaoId)
        {
            AdesaoId = adesaoId;
        }
        #endregion
    }
}
