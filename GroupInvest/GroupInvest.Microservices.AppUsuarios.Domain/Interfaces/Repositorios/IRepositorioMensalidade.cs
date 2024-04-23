using GroupInvest.Common.Domain.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios
{
    /// <summary>
    /// Interface para os repositórios de mensalidades
    /// </summary>
    public interface IRepositorioMensalidade : IMongoRepositorio<Mensalidade>
    {
        /// <summary>
        /// Inclui os dados de pagamento em uma mensalidade
        /// </summary>
        /// <param name="mensalidade">Mensalidade</param>
        /// <param name="pagamento">Dados de pagamento</param>
        /// <returns>Resultado da operação</returns>
        void IncluirPagamento(Mensalidade mensalidade, PagamentoMensalidade pagamento);

        /// <summary>
        /// Lista as mensalidades de um participante
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Lista de mensalidades</returns>
        IReadOnlyCollection<Mensalidade> ListarMensalidadesParticipante(int participanteId);

        /// <summary>
        /// Obtém uma mensalidade pelo identificador
        /// </summary>
        /// <param name="id">Identificador da mensalidade</param>
        /// <returns>Mensalidade</returns>
        Mensalidade ObterMensalidadePorId(int id);

        /// <summary>
        /// Lista as mensalidades do participante que já foram pagas
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Valor acumulado</returns>
        IReadOnlyCollection<Mensalidade> ListarMensalidadesPagasParticipante(int participanteId);
    }
}
