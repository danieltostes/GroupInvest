using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Events.Mensalidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para o handler de eventos de mensalidade
    /// </summary>
    public interface IMensalidadeEventHandler
    {
        OperationResult Handle(MensalidadesGeradasEvent evento);
    }
}
