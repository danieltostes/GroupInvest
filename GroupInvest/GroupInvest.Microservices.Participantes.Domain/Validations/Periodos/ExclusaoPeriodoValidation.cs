using GroupInvest.Common.Domain.Validations;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Domain.Validations.Periodos
{
    public class ExclusaoPeriodoValidation : ValidationBase<Periodo>
    {
        public ExclusaoPeriodoValidation(IRepositorioAdesao repositorioAdesao)
        {
            specifications.Add(new PeriodoSpecificationSemAdesaoAtiva(repositorioAdesao));
        }
    }
}
