using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Application.Events.Participantes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Application.Models.Interfaces
{
    /// <summary>
    /// Interface para o handler de eventos dos participantes
    /// </summary>
    public interface IParticipanteEventHandler
    {
        OperationResult Handle(ParticipanteIncluidoEvent evento);
        OperationResult Handle(ParticipanteAlteradoEvent evento);
        OperationResult Handle(ParticipanteInativadoEvent evento);
        OperationResult Handle(AdesaoRealizadaEvent evento);
        OperationResult Handle(AdesaoCanceladaEvent evento);
    }
}
