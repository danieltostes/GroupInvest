using GroupInvest.Common.Domain.Validations;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Specifications;

namespace GroupInvest.Microservices.Participantes.Domain.Validations.Participantes
{
    public class InativacaoParticipanteValidation : ValidationBase<Participante>
    {
        public InativacaoParticipanteValidation(IRepositorioPeriodo repositorioPeriodo, IRepositorioAdesao repositorioAdesao)
        {
            specifications.Add(new ParticipanteSpecificationSemAdesaoAtiva(repositorioPeriodo, repositorioAdesao));
            specifications.Add(new ParticipanteSpecificationStatusAtivo());
        }
    }
}
