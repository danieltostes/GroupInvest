using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Mensalidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers
{
    /// <summary>
    /// Interface para EventHandler de mensalidades
    /// </summary>
    public interface IMensalidadeEventHandler
    {
        OperationResult Handle(IntegracaoMensalidadesGeradasEvent evento);
        OperationResult Handle(MensalidadesRegistradasEvent evento);
    }
}
