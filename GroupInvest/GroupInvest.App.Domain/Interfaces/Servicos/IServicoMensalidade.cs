using GroupInvest.App.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Domain.Interfaces.Servicos
{
    /// <summary>
    /// Interface para os serviços de mensalidade
    /// </summary>
    public interface IServicoMensalidade
    {
        /// <summary>
        /// Token para as requisições http
        /// </summary>
        string Token { get; set; }

        /// <summary>
        /// Lista todas as mensalidades de um participante
        /// </summary>
        /// <param name="participanteId">Identificador do participante</param>
        /// <returns>Lista de mensalidades</returns>
        IReadOnlyCollection<Mensalidade> ListarMensalidadesParticipante(int participanteId);
    }
}
