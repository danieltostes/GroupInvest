using GroupInvest.Common.Domain.Validations;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Specifications;

namespace GroupInvest.Microservices.Participantes.Domain.Validations.Participantes
{
    public class PersistenciaParticipanteValidation : ValidationBase<Participante>
    {
        public PersistenciaParticipanteValidation(IRepositorioParticipante repositorioParticipante)
        {
            specifications.Add(new ParticipanteSpecificationNomePreenchido());
            specifications.Add(new ParticipanteSpecificationNomeMinimoCaracteres());
            specifications.Add(new ParticipanteSpecificationTelefoneMinimoCaracteres());
            specifications.Add(new ParticipanteSpecificationEmailUnico(repositorioParticipante));
            specifications.Add(new ParticipanteSpecificationStatusAtivo());
        }
    }
}
