using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Events.Participantes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para o handler de eventos de Participante
    /// </summary>
    public interface IParticipanteEventHandler
    {
        OperationResult Handle(ParticipanteIncluidoEvent evento);
    }
}
