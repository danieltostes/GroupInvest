using GroupInvest.Common.Domain.Specifications;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Domain.Specifications
{
    // Número de cotas maior que zero
    public class AdesaoSpecificationNumeroMinimoCotas : SpecificationBase<Adesao>
    {
        public override bool IsSatisfiedBy(Adesao entidade)
        {
            if (entidade.NumeroCotas <= 0)
                AddMessage(Mensagens.AdesaoNumeroMinimoCotas);

            return Messages.Count == 0;
        }
    }

    // Participante informado
    public class AdesaoSpecificationParticipanteInformado : SpecificationBase<Adesao>
    {
        public override bool IsSatisfiedBy(Adesao entidade)
        {
            if (entidade.Participante == null)
                AddMessage(Mensagens.AdesaoSemParticipante);

            return Messages.Count == 0;
        }
    }

    // Período informado
    public class AdesaoSpecificationPeriodoInformado : SpecificationBase<Adesao>
    {
        public override bool IsSatisfiedBy(Adesao entidade)
        {
            if (entidade.Periodo == null)
                AddMessage(Mensagens.AdesaoSemPeriodo);

            return Messages.Count == 0;
        }
    }

    // Adesão existente
    public class AdesaoSpecificationAdesaoUnica : SpecificationBase<Adesao>
    {
        private readonly IRepositorioAdesao repositorioAdesao;

        public AdesaoSpecificationAdesaoUnica(IRepositorioAdesao repositorioAdesao)
        {
            this.repositorioAdesao = repositorioAdesao;
        }

        public override bool IsSatisfiedBy(Adesao entidade)
        {
            var adesaoExistente = repositorioAdesao.ObterAdesao(entidade.Participante, entidade.Periodo);
            if (adesaoExistente != null)
                AddMessage(Mensagens.AdesaoExistente);

            return Messages.Count == 0;
        }
    }
}
