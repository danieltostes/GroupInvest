using GroupInvest.Common.Domain.Validations;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Specifications;

namespace GroupInvest.Microservices.Participantes.Domain.Validations.Participantes
{
    public class RealizarAdesaoValidation : ValidationBase<Adesao>
    {
        public RealizarAdesaoValidation(IRepositorioAdesao repositorioAdesao)
        {
            specifications.Add(new AdesaoSpecificationParticipanteInformado());
            specifications.Add(new AdesaoSpecificationPeriodoInformado());
            specifications.Add(new AdesaoSpecificationNumeroMinimoCotas());
            specifications.Add(new AdesaoSpecificationAdesaoUnica(repositorioAdesao));
        }
    }
}
