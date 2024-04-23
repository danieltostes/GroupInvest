using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Events.RendimentoParcial;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers
{
    /// <summary>
    /// Interface para EventHandler de Rendimento Parcial
    /// </summary>
    public interface IRendimentoParcialEventHandler
    {
        OperationResult Handle(IntegracaoRendimentoParcialEvent evento);
        OperationResult Handle(RendimentoParcialRegistradoEvent evento);
    }
}
