using AutoMapper;
using GroupInvest.Microservices.Mensalidades.Application.Interfaces;
using GroupInvest.Microservices.Mensalidades.Application.Models.Dtos;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application
{
    public class MensalidadeApi : IMensalidadeApi
    {
        #region Injeção de dependência
        private readonly IMapper mapper;
        private readonly IServicoAdesao servicoAdesao;
        private readonly IServicoParticipante servicoParticipante;
        private readonly IServicoMensalidade servicoMensalidade;
        #endregion

        #region Construtor
        public MensalidadeApi(IMapper mapper, IServicoAdesao servicoAdesao, IServicoParticipante servicoParticipante, IServicoMensalidade servicoMensalidade)
        {
            this.mapper = mapper;
            this.servicoAdesao = servicoAdesao;
            this.servicoParticipante = servicoParticipante;
            this.servicoMensalidade = servicoMensalidade;
        }
        #endregion

        #region IMensalidadeApi
        public IReadOnlyCollection<MensalidadeDto> ListarMensalidadesParticipante(int participanteId)
        {
            var participante = servicoParticipante.ObterParticipantePorId(participanteId);
            if (participante == null)
                return null;

            var adesaoAtiva = servicoAdesao.ObterAdesaoAtiva(participante);
            if (adesaoAtiva == null)
                return null;

            var mensalidades = servicoMensalidade.ListarMensalidadesAdesao(adesaoAtiva);
            return mapper.Map<IReadOnlyCollection<Mensalidade>, IReadOnlyCollection<MensalidadeDto>>(mensalidades);
        }
        #endregion
    }
}
