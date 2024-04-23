using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos
{
    /// <summary>
    /// Interface para o serviço de Adesão
    /// </summary>
    public interface IServicoAdesao
    {
        /// <summary>
        /// Realiza a adesão de um participante a um período
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <param name="periodo">Período</param>
        /// <param name="numeroCotas">Número de cotas</param>
        /// <returns>Resultado da operação</returns>
        OperationResult RealizarAdesao(Participante participante, Periodo periodo, int numeroCotas);

        /// <summary>
        /// Obtém uma adesão pelo identificador
        /// </summary>
        /// <param name="id">Identificador da adesão</param>
        /// <returns>Adesão</returns>
        Adesao ObterAdesaoPorId(int id);

        /// <summary>
        /// Obtém uma adesão por sua chave lógica
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <param name="periodoId">Identificador do período</param>
        /// <returns>Adesão</returns>
        Adesao ObterAdesao(int participanteId, int periodoId);

        /// <summary>
        /// Obtem a adeão ativa do participante
        /// </summary>
        /// <param name="participante">Participante</param>
        /// <returns>Adesão ativa</returns>
        Adesao ObterAdesaoAtiva(Participante participante);

        /// <summary>
        /// Lista todas as adesões de um período
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <returns>Lista de adesões</returns>
        IReadOnlyCollection<Adesao> ListarAdesoesPeriodo(Periodo periodo);

        /// <summary>
        /// Obtém o número total de cotas de um período
        /// </summary>
        /// <param name="periodo">Período</param>
        /// <returns>Número total de cotas</returns>
        int ObterTotalCotasPeriodo(Periodo periodo);
    }
}
