using AutoMapper;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Application.Commands.Participantes;
using GroupInvest.Microservices.Participantes.Application.Interfaces;
using GroupInvest.Microservices.Participantes.Application.Models.Dtos;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Servicos;
using System.Collections.Generic;
using System.Linq;

namespace GroupInvest.Microservices.Participantes.Application
{
    public class ParticipanteApi : IParticipanteApi
    {
        private readonly IMediatorHandler bus;
        private readonly IServicoParticipante servicoParticipante;
        private readonly IMapper mapper;

        public ParticipanteApi(IMediatorHandler bus, IServicoParticipante servicoParticipante, IMapper mapper)
        {
            this.bus = bus;
            this.servicoParticipante = servicoParticipante;
            this.mapper = mapper;
        }

        #region Commands
        public OperationResult IncluirParticipante(ParticipanteDto dto)
        {
            var command = new IncluirParticipanteCommand(dto.Nome, dto.Email, dto.Telefone);
            return bus.SendCommand(command);
        }

        public OperationResult AlterarParticipante(ParticipanteDto dto)
        {
            var command = new AlterarParticipanteCommand(dto.Id, dto.Nome, dto.Email, dto.Telefone, dto.UsuarioAplicativoId);
            return bus.SendCommand(command);
        }

        public OperationResult InativarParticipante(int id)
        {
            var command = new InativarParticipanteCommand(id);
            return bus.SendCommand(command);
        }

        public OperationResult RealizarAdesaoParticipantePeriodoAtivo(int participanteId, int numeroCotas)
        {
            var command = new RealizarAdesaoParticipanteCommand(participanteId, numeroCotas);
            return bus.SendCommand(command);
        }

        public OperationResult CancelarAdesaoParticipantePeriodoAtivo(int participanteId)
        {
            var command = new CancelarAdesaoParticipanteCommand(participanteId);
            return bus.SendCommand(command);
        }
        #endregion

        #region Queries
        public ParticipanteDto ObterParticipantePorId(int id)
        {
            var participante = servicoParticipante.ObterParticipantePorId(id);
            if (participante != null)
            {
                var dto = mapper.Map<ParticipanteDto>(participante);
                return dto;
            }
            return null;
        }

        public ParticipanteDto ObterParticipantePorEmail(string email)
        {
            var participante = servicoParticipante.ObterParticipantePorEmail(email);
            if (participante != null)
            {
                var dto = mapper.Map<ParticipanteDto>(participante);
                return dto;
            }
            return null;
        }

        public ParticipanteDto ObterParticipantePorUsuarioAplicativo(string userId)
        {
            var participante = servicoParticipante.ObterParticipantePorUsuarioAplicativo(userId);
            if (participante != null)
            {
                var dto = mapper.Map<ParticipanteDto>(participante);
                return dto;
            }
            return null;
        }

        public AdesaoDto ObterAdesaoAtivaParticipante(int participanteId)
        {
            var participante = servicoParticipante.ObterParticipantePorId(participanteId);
            if (participante != null)
            {
                var adesao = servicoParticipante.ObterAdesaoAtivaParticipante(participante);
                return mapper.Map<Adesao, AdesaoDto>(adesao);
            }
            else
                return null;
        }

        public IReadOnlyCollection<ParticipanteDto> ListarParticipantesAtivos()
        {
            var participantes = servicoParticipante.ListarParticipantesAtivos();
            return mapper.Map<IReadOnlyCollection<Participante>, IReadOnlyCollection<ParticipanteDto>>(participantes);
        }

        public IReadOnlyCollection<ParticipanteDto> ListarTodosParticipantes()
        {
            var participantes = servicoParticipante.ListarParticipantes();
            return mapper.Map<IReadOnlyCollection<Participante>, IReadOnlyCollection<ParticipanteDto>>(participantes);
        }
        #endregion
    }
}
