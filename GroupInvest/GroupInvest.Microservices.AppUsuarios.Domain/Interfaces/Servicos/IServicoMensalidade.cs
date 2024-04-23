using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos
{
    /// <summary>
    /// Interface para o serviço de mensalidades
    /// </summary>
    public interface IServicoMensalidade
    {
        /// <summary>
        /// Inclui uma mensalidade na base de dados
        /// </summary>
        /// <param name="mensalidade">Mensalidade</param>
        /// <returns>Resultado da Operação</returns>
        OperationResult IncluirMensalidade(Mensalidade mensalidade);
        
        /// <summary>
        /// Altera uma mensalidade na base de dados
        /// </summary>
        /// <param name="mensalidade">Mensalidade</param>
        /// <returns>Resultado da operação</returns>
        OperationResult AlterarMensalidade(Mensalidade mensalidade);

        /// <summary>
        /// Inclui os dados de pagamento a uma mensalidade
        /// </summary>
        /// <param name="mensalidade">Mensalidade</param>
        /// <param name="pagamento">Dados do pagamento</param>
        /// <returns>Resultado da operação</returns>
        OperationResult IncluirPagamento(Mensalidade mensalidade, PagamentoMensalidade pagamento);

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
        /// Obtém o valor acumulado pelo participante
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Valor total acumulado</returns>
        decimal ObterValorAcumuladoParticipante(int participanteId);
    }
}
