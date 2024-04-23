using GroupInvest.Common.Domain.Validations;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Specifications;

namespace GroupInvest.Microservices.Participantes.Domain.Validations.Periodos
{
    public class PersistenciaPeriodoValidation : ValidationBase<Periodo>
    {
        public PersistenciaPeriodoValidation(IRepositorioPeriodo repositorioPeriodo)
        {
            specifications.Add(new PeriodoSpecificationDataInicioInformada());
            specifications.Add(new PeriodoSpecificationDataTerminoInformada());
            specifications.Add(new PeriodoSpecificationAnoReferenciaValido());
            specifications.Add(new PeriodoSpecificationAnoReferenciaUnico(repositorioPeriodo));
        }
    }
}
