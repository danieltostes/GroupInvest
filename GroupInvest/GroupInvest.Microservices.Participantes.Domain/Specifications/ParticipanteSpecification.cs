using GroupInvest.Common.Domain.Specifications;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Resources;

namespace GroupInvest.Microservices.Participantes.Domain.Specifications
{
    // Nome preenchido
    public class ParticipanteSpecificationNomePreenchido : SpecificationBase<Participante>
    {
        public override bool IsSatisfiedBy(Participante entidade)
        {
            if (string.IsNullOrEmpty(entidade.Nome))
                AddMessage(Mensagens.ParticipanteNomeNulo);

            return Messages.Count == 0;
        }
    }

    // Nome com menos de 3 caracteres
    public class ParticipanteSpecificationNomeMinimoCaracteres : SpecificationBase<Participante>
    {
        public override bool IsSatisfiedBy(Participante entidade)
        {
            if (!string.IsNullOrEmpty(entidade.Nome) && entidade.Nome.Length < 3)
                AddMessage(Mensagens.ParticipanteNomeMinimoCaracteres);

            return Messages.Count == 0;
        }
    }

    // Telefone com menos de 9 caracteres
    public class ParticipanteSpecificationTelefoneMinimoCaracteres : SpecificationBase<Participante>
    {
        public override bool IsSatisfiedBy(Participante entidade)
        {
            if (!string.IsNullOrEmpty(entidade.Telefone) && entidade.Telefone.Length < 9)
                AddMessage(Mensagens.ParticipanteTelefoneMinimoCaracteres);

            return Messages.Count == 0;
        }
    }

    // Status ativo
    public class ParticipanteSpecificationStatusAtivo : SpecificationBase<Participante>
    {
        public override bool IsSatisfiedBy(Participante entidade)
        {
            if (!entidade.Ativo)
                AddMessage(Mensagens.ParticipanteStatusAtivo);

            return Messages.Count == 0;
        }
    }

    // Email único do participante
    public class ParticipanteSpecificationEmailUnico : SpecificationBase<Participante>
    {
        private readonly IRepositorioParticipante repositorio;

        public ParticipanteSpecificationEmailUnico(IRepositorioParticipante repositorio)
        {
            this.repositorio = repositorio;
        }

        public override bool IsSatisfiedBy(Participante entidade)
        {
            var participante = repositorio.ObterParticipantePorEmail(entidade.Email);
            if (participante != null && participante.Id != entidade.Id)
                AddMessage(Mensagens.ParticipanteEmailExistente);

            return Messages.Count == 0;
        }
    }

    // Participante sem adesão ativa
    public class ParticipanteSpecificationSemAdesaoAtiva : SpecificationBase<Participante>
    {
        private readonly IRepositorioPeriodo repositorioPeriodo;
        private readonly IRepositorioAdesao repositorioAdesao;

        public ParticipanteSpecificationSemAdesaoAtiva(IRepositorioPeriodo repositorioPeriodo, IRepositorioAdesao repositorioAdesao)
        {
            this.repositorioPeriodo = repositorioPeriodo;
            this.repositorioAdesao = repositorioAdesao;
        }

        public override bool IsSatisfiedBy(Participante entidade)
        {
            var periodoAtivo = repositorioPeriodo.ObterPeriodoAtivo();
            if (periodoAtivo != null)
            {
                var adesaoAtiva = repositorioAdesao.ObterAdesao(entidade, periodoAtivo);
                if (adesaoAtiva != null)
                    AddMessage(Mensagens.ParticipanteComAdesaoAtiva);
            }
            return Messages.Count == 0;
        }
    }
}
