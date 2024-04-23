using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Auditoria.Application.Commands.AuditoriaBase;
using GroupInvest.Microservices.Auditoria.Application.Events;
using GroupInvest.Microservices.Auditoria.Application.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using EventHandler = GroupInvest.Common.Application.EventHandlers.EventHandler;

namespace GroupInvest.Microservices.Auditoria.Application.EventHandlers
{
    public class AuditoriaBaseEventHandler : EventHandler, IAuditoriaBaseEventHandler
    {
        private readonly IMediatorHandler bus;

        #region Construtor
        public AuditoriaBaseEventHandler(IMediatorHandler bus)
        {
            this.bus = bus;
        }
        #endregion

        public OperationResult Handle(RegistroAuditoriaBaseIncluidoEvent evento)
        {
            return OperationResult.OK;
        }

        public OperationResult Handle(AlteracaoDadosEvent evento)
        {
            return bus.SendCommand(new IncluirAuditoriaBaseCommand(
                evento.Auditoria.AgregadoId,
                evento.Auditoria.Agregado,
                evento.Auditoria.Operacao,
                evento.Auditoria.Conteudo,
                evento.Auditoria.Timestamp));
        }
    }
}
