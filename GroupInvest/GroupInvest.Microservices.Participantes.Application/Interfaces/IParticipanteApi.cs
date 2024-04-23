using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.Participantes.Application.Interfaces
{
    /// <summary>
    /// Interface para a API de participantes
    /// </summary>
    public interface IParticipanteApi
    {
        OperationResult IncluirParticipante(ParticipanteDto dto);
        OperationResult AlterarParticipante(ParticipanteDto dto);
        OperationResult InativarParticipante(int id);
        OperationResult RealizarAdesaoParticipantePeriodoAtivo(int participanteId, int numeroCotas);
        OperationResult CancelarAdesaoParticipantePeriodoAtivo(int participanteId);
        ParticipanteDto ObterParticipantePorId(int id);
        ParticipanteDto ObterParticipantePorEmail(string email);
        ParticipanteDto ObterParticipantePorUsuarioAplicativo(string userId);
        AdesaoDto ObterAdesaoAtivaParticipante(int participanteId);
        IReadOnlyCollection<ParticipanteDto> ListarParticipantesAtivos();
        IReadOnlyCollection<ParticipanteDto> ListarTodosParticipantes();
    }
}
