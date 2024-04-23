using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.Participantes.Domain.Interfaces.Servicos
{
    /// <summary>
    /// Interface para serviço de participantes
    /// </summary>
    public interface IServicoParticipante
    {
        /// <summary>
        /// Inclui um participante
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <returns>Resultado da operação</returns>
        OperationResult IncluirParticipante(Participante participante);

        /// <summary>
        /// Altera um participante
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <returns>Resultado da operação</returns>
        OperationResult AlterarParticipante(Participante participante);

        /// <summary>
        /// Inativa um participante
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <returns>Resultado da operação</returns>
        OperationResult InativarParticipante(Participante participante);

        /// <summary>
        /// Realiza a adesão do participante no período ativo
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <param name="numeroCotas">Número de cotas desejadas pelo participante</param>
        /// <returns>Resultado da operação</returns>
        OperationResult RealizarAdesaoParticipantePeriodoAtivo(Participante participante, int numeroCotas);

        /// <summary>
        /// Cancela a adesão do participante no período ativo
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <returns>Resultado da operação</returns>
        OperationResult CancelarAdesaoParticipantePeriodoAtivo(Participante participante);

        /// <summary>
        /// Obtém um participante pelo identificador
        /// </summary>
        /// <param name="id">Identificador do participante</param>
        /// <returns>Participante</returns>
        Participante ObterParticipantePorId(int id);

        /// <summary>
        /// Obtém um participante pelo email
        /// </summary>
        /// <param name="email">Email do participante</param>
        /// <returns>Participante</returns>
        Participante ObterParticipantePorEmail(string email);

        /// <summary>
        /// Obtém um participante pelo identificador do usuário de aplicativo no IAm
        /// </summary>
        /// <param name="userId">Identificador do usuário no IAm</param>
        /// <returns>Participante</returns>
        Participante ObterParticipantePorUsuarioAplicativo(string userId);

        /// <summary>
        /// Obtém a adesão ativa do participante
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <returns>Adesão ativa</returns>
        Adesao ObterAdesaoAtivaParticipante(Participante participante);

        /// <summary>
        /// Obtém a adesão do participante em um período específico
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <param name="periodo">Período</param>
        /// <returns>Adesão</returns>
        Adesao ObterAdesaoParticipantePeriodo(Participante participante, Periodo periodo);

        /// <summary>
        /// Lista todos os participantes do período ativo
        /// </summary>
        /// <returns>Lista de participantes</returns>
        IReadOnlyCollection<Participante> ListarParticipantesAtivos();

        /// <summary>
        /// Lista todos os participantes cadastrados
        /// </summary>
        /// <returns>Participantes</returns>
        IReadOnlyCollection<Participante> ListarParticipantes();
    }
}
