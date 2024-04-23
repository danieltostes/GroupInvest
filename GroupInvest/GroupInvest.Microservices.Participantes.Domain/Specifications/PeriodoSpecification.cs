using GroupInvest.Common.Domain.Specifications;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Domain.Specifications
{
    // Data de ínício preenchida
    public class PeriodoSpecificationDataInicioInformada : SpecificationBase<Periodo>
    {
        public override bool IsSatisfiedBy(Periodo entidade)
        {
            if (!entidade.DataInicio.HasValue)
                AddMessage(Mensagens.PeriodoDataInicioNula);

            return Messages.Count == 0;
        }
    }

    // Data de término preenchida
    public class PeriodoSpecificationDataTerminoInformada : SpecificationBase<Periodo>
    {
        public override bool IsSatisfiedBy(Periodo entidade)
        {
            if (!entidade.DataTermino.HasValue)
                AddMessage(Mensagens.PeriodoDataTerminoNula);

            return Messages.Count == 0;
        }
    }

    // Ano de referência igual ou superior a 2020
    public class PeriodoSpecificationAnoReferenciaValido : SpecificationBase<Periodo>
    {
        public override bool IsSatisfiedBy(Periodo entidade)
        {
            if (entidade.AnoReferencia < 2020)
                AddMessage(Mensagens.PeriodoAnoReferenciaInvalido);

            return Messages.Count == 0;
        }
    }

    // Ano de referência único
    public class PeriodoSpecificationAnoReferenciaUnico : SpecificationBase<Periodo>
    {
        private readonly IRepositorioPeriodo repositorioPeriodo;

        public PeriodoSpecificationAnoReferenciaUnico(IRepositorioPeriodo repositorioPeriodo)
        {
            this.repositorioPeriodo = repositorioPeriodo;
        }

        public override bool IsSatisfiedBy(Periodo entidade)
        {
            var periodoExistente = repositorioPeriodo.ObterPeriodoPorAnoReferencia(entidade.AnoReferencia);
            if (periodoExistente != null && periodoExistente.Id != entidade.Id)
                AddMessage(Mensagens.PeriodoAnoReferenciaExistente);

            return Messages.Count == 0;
        }
    }

    // Periodo sem adesão ativa
    public class PeriodoSpecificationSemAdesaoAtiva : SpecificationBase<Periodo>
    {
        private readonly IRepositorioAdesao repositorioAdesao;

        public PeriodoSpecificationSemAdesaoAtiva(IRepositorioAdesao repositorioAdesao)
        {
            this.repositorioAdesao = repositorioAdesao;
        }

        public override bool IsSatisfiedBy(Periodo entidade)
        {
            var adesoesAtivas = repositorioAdesao.ListarAdesoesAtivasPeriodo(entidade);
            if (adesoesAtivas.Count > 0)
                AddMessage(Mensagens.PeriodoPossuiAdesoesAtivas);

            return Messages.Count == 0;
        }
    }
}
