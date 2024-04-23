using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos
{
    /// <summary>
    /// Interface para o serviço de rendimento parcial de periodo
    /// </summary>
    public interface IServicoRendimentoParcialPeriodo
    {
        /// <summary>
        /// Registra um rendimento parcial no repositório
        /// </summary>
        /// <param name="rendimento">Rendimento parcial do periodo</param>
        /// <returns>Resultado da operação</returns>
        OperationResult RegistrarRendimentoParcial(RendimentoParcialPeriodo rendimento);

        /// <summary>
        /// Lista os rendimentos parciais do período
        /// </summary>
        /// <returns>Lista de rendimentos parciais</returns>
        IReadOnlyCollection<RendimentoParcialPeriodo> ListarRendimentosParciais();
    }
}
