using AutoMapper;
using GroupInvest.Common.Application.CommandHandlers;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Auditoria.Application.Commands.AuditoriaBase;
using GroupInvest.Microservices.Auditoria.Application.Events;
using GroupInvest.Microservices.Auditoria.Application.Models.Interfaces;
using GroupInvest.Microservices.Auditoria.Domain.Entidades;
using GroupInvest.Microservices.Auditoria.Domain.Interfaces.Servicos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Auditoria.Application.CommandHandlers
{
    public class AuditoriaBaseCommandHandler : CommandHandler, IAuditoriaBaseCommandHandler
    {
        private readonly IMediatorHandler bus;
        private readonly IMapper mapper;
        private readonly IServicoAuditoria servicoAuditoria;

        #region Construtor
        public AuditoriaBaseCommandHandler(IMediatorHandler bus, IMapper mapper, IServicoAuditoria servicoAuditoria)
        {
            this.bus = bus;
            this.mapper = mapper;
            this.servicoAuditoria = servicoAuditoria;
        }
        #endregion

        public OperationResult Handle(IncluirAuditoriaBaseCommand command)
        {
            var auditoria = mapper.Map<IncluirAuditoriaBaseCommand, AuditoriaBase>(command);
            var result = servicoAuditoria.IncluirAuditoria(auditoria);

            if (result.StatusCode == StatusCodeEnum.OK)
            {
                bus.PublishEvent(new RegistroAuditoriaBaseIncluidoEvent
                {
                    Id = auditoria.Id,
                    AgregadoId = auditoria.AgregadoId,
                    Operacao = "Registro de Auditoria",
                    Timestamp = DateTime.Now,
                    Conteudo = JsonConvert.SerializeObject(auditoria)
                });
            }

            return result;
        }
    }
}
