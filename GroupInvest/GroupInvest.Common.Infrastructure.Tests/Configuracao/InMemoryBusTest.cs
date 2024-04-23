using AutoMapper;
using GroupInvest.Common.Application.Commands;
using GroupInvest.Common.Application.Events;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Common.Infrastructure.Messaging;
using GroupInvest.Microservices.Auditoria.Infra.DataAccess.Contextos;
using GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Contextos;
using GroupInvest.Microservices.Participantes.Infra.DataAccess.Contextos;
using Microsoft.Extensions.Configuration;

using AuditoriaDomain = GroupInvest.Microservices.Auditoria.Domain;
using AuditoriaCommand = GroupInvest.Microservices.Auditoria.Application.Commands;
using AuditoriaCommandHandler = GroupInvest.Microservices.Auditoria.Application.CommandHandlers;
using AuditoriaEvent = GroupInvest.Microservices.Auditoria.Application.Events;
using AuditoriaEventHandler = GroupInvest.Microservices.Auditoria.Application.EventHandlers;
using AuditoriaInfra = GroupInvest.Microservices.Auditoria.Infra;

using ParticipantesDomain = GroupInvest.Microservices.Participantes.Domain;
using ParticipantesCommand = GroupInvest.Microservices.Participantes.Application.Commands;
using ParticipantesCommandHandler = GroupInvest.Microservices.Participantes.Application.CommandHandlers;
using ParticipantesEvent = GroupInvest.Microservices.Participantes.Application.Events;
using ParticipantesEventHandler = GroupInvest.Microservices.Participantes.Application.EventHandlers;
using ParticipantesInfra = GroupInvest.Microservices.Participantes.Infra;

using MensalidadesDomain = GroupInvest.Microservices.Mensalidades.Domain;
using MensalidadesCommand = GroupInvest.Microservices.Mensalidades.Application.Commands;
using MensalidadesCommandHandler = GroupInvest.Microservices.Mensalidades.Application.CommandHandlers;
using MensalidadesEvent = GroupInvest.Microservices.Mensalidades.Application.Events;
using MensalidadesEventHandler = GroupInvest.Microservices.Mensalidades.Application.EventHandlers;
using MensalidadesInfra = GroupInvest.Microservices.Mensalidades.Infra;

namespace GroupInvest.Common.Infrastructure.Tests.Configuracao
{
    // classe de bus sem envio pra fila pra testes unitarios
    public class BusService : IMediatorHandlerQueue
    {
        public OperationResult PublishEvent<T>(T evento) where T : Event
        {
            return OperationResult.OK;
        }

        public OperationResult SendCommand<T>(T command) where T : Command
        {
            return OperationResult.OK;
        }
    }

    public class InMemoryBusTest : InMemoryBus
    {
        private static InMemoryBusTest instance;

        protected override void ConfigureHandlers()
        {
            // Configuração do UserSecrets
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets(this.GetType().Assembly);
            var configuration = builder.Build();

            // DbContext
            AddDbContext<MicroserviceParticipantesDbContext>("GroupInvest.Microservices.Participantes", configuration["ParticipantesDbConnectionString"]);
            AddDbContext<MicroserviceAuditoriaDbContext>("GroupInvest.Microservices.Auditoria", configuration["AuditoriaDbConnectionString"]);
            AddDbContext<MicroserviceMensalidadesDbContext>("GroupInvest.Microservices.Mensalidades", configuration["MensalidadesDbConnectionString"]);

            // CommandHandlers
            SetCommandHandler<ParticipantesCommand.Participantes.IncluirParticipanteCommand, ParticipantesCommandHandler.ParticipanteCommandHandler>();
            SetCommandHandler<ParticipantesCommand.Participantes.AlterarParticipanteCommand, ParticipantesCommandHandler.ParticipanteCommandHandler>();
            SetCommandHandler<ParticipantesCommand.Participantes.InativarParticipanteCommand, ParticipantesCommandHandler.ParticipanteCommandHandler>();
            SetCommandHandler<ParticipantesCommand.Participantes.RealizarAdesaoParticipanteCommand, ParticipantesCommandHandler.ParticipanteCommandHandler>();
            SetCommandHandler<ParticipantesCommand.Participantes.CancelarAdesaoParticipanteCommand, ParticipantesCommandHandler.ParticipanteCommandHandler>();
            SetCommandHandler<ParticipantesCommand.Periodos.IncluirPeriodoCommand, ParticipantesCommandHandler.PeriodoCommandHandler>();
            SetCommandHandler<ParticipantesCommand.Periodos.AlterarPeriodoCommand, ParticipantesCommandHandler.PeriodoCommandHandler>();
            SetCommandHandler<ParticipantesCommand.Periodos.EncerrarPeriodoCommand, ParticipantesCommandHandler.PeriodoCommandHandler>();

            SetCommandHandler<AuditoriaCommand.AuditoriaBase.IncluirAuditoriaBaseCommand, AuditoriaCommandHandler.AuditoriaBaseCommandHandler>();

            SetCommandHandler<MensalidadesCommand.Periodos.IncluirPeriodoCommand, MensalidadesCommandHandler.PeriodoCommandHandler>();
            SetCommandHandler<MensalidadesCommand.Participantes.IncluirParticipanteCommand, MensalidadesCommandHandler.ParticipanteCommandHandler>();
            SetCommandHandler<MensalidadesCommand.Mensalidades.GerarMensalidadesCommand, MensalidadesCommandHandler.MensalidadeCommandHandler>();

            // EventHandlers
            SetEventHandler<ParticipantesEvent.Participantes.ParticipanteIncluidoEvent, ParticipantesEventHandler.ParticipanteEventHandler>();
            SetEventHandler<ParticipantesEvent.Participantes.ParticipanteAlteradoEvent, ParticipantesEventHandler.ParticipanteEventHandler>();
            SetEventHandler<ParticipantesEvent.Participantes.ParticipanteInativadoEvent, ParticipantesEventHandler.ParticipanteEventHandler>();
            SetEventHandler<ParticipantesEvent.Participantes.AdesaoRealizadaEvent, ParticipantesEventHandler.ParticipanteEventHandler>();
            SetEventHandler<ParticipantesEvent.Participantes.AdesaoCanceladaEvent, ParticipantesEventHandler.ParticipanteEventHandler>();
            SetEventHandler<ParticipantesEvent.Periodos.PeriodoIncluidoEvent, ParticipantesEventHandler.PeriodoEventHandler>();
            SetEventHandler<ParticipantesEvent.Periodos.PeriodoAlteradoEvent, ParticipantesEventHandler.PeriodoEventHandler>();
            SetEventHandler<ParticipantesEvent.Periodos.PeriodoEncerradoEvent, ParticipantesEventHandler.PeriodoEventHandler>();

            SetEventHandler<AuditoriaEvent.RegistroAuditoriaBaseIncluidoEvent, AuditoriaEventHandler.AuditoriaBaseEventHandler>();

            // Subscribers (Comentar quando estiver trabalhando com filas)
            //SetSubscriber<AuditoriaEventHandler.AuditoriaBaseEventHandler>("AlteracaoDadosEvent");
            //SetSubscriber<MensalidadesEventHandler.AdesaoEventHandler>("AdesaoRealizadaEvent");

            // Serviços
            AddDependencyInjection<ParticipantesDomain.Interfaces.Servicos.IServicoParticipante, ParticipantesDomain.Servicos.ServicoParticipante>();
            AddDependencyInjection<ParticipantesDomain.Interfaces.Servicos.IServicoPeriodo, ParticipantesDomain.Servicos.ServicoPeriodo>();

            AddDependencyInjection<AuditoriaDomain.Interfaces.Servicos.IServicoAuditoria, AuditoriaDomain.Servicos.ServicoAuditoria>();

            AddDependencyInjection<MensalidadesDomain.Interfaces.Servicos.IServicoParticipante, MensalidadesDomain.Servicos.ServicoParticipante>();
            AddDependencyInjection<MensalidadesDomain.Interfaces.Servicos.IServicoPeriodo, MensalidadesDomain.Servicos.ServicoPeriodo>();
            AddDependencyInjection<MensalidadesDomain.Interfaces.Servicos.IServicoMensalidade, MensalidadesDomain.Servicos.ServicoMensalidade>();

            // Repositórios
            AddDependencyInjection<ParticipantesDomain.Interfaces.Repositorios.IRepositorioParticipante, ParticipantesInfra.DataAccess.Repositorios.RepositorioParticipante>();
            AddDependencyInjection<ParticipantesDomain.Interfaces.Repositorios.IRepositorioPeriodo, ParticipantesInfra.DataAccess.Repositorios.RepositorioPeriodo>();
            AddDependencyInjection<ParticipantesDomain.Interfaces.Repositorios.IRepositorioAdesao, ParticipantesInfra.DataAccess.Repositorios.RepositorioAdesao>();

            AddDependencyInjection<AuditoriaDomain.Interfaces.Repositorios.IRepositorioAuditoria, AuditoriaInfra.DataAccess.Repositorios.RepositorioAuditoria>();

            AddDependencyInjection<MensalidadesDomain.Interfaces.Repositorios.IRepositorioParticipante, MensalidadesInfra.DataAccess.Repositorios.RepositorioParticipante>();
            AddDependencyInjection<MensalidadesDomain.Interfaces.Repositorios.IRepositorioPeriodo, MensalidadesInfra.DataAccess.Repositorios.RepositorioPeriodo>();
            AddDependencyInjection<MensalidadesDomain.Interfaces.Repositorios.IRepositorioMensalidade, MensalidadesInfra.DataAccess.Repositorios.RepositorioMensalidade>();

            // AutoMapper
            AddDependencyInjection<IMapper>(AutoMapperConfig.Mapper());

            // ServiceBus (Azure)
            AddDependencyInjection<IMediatorHandlerQueue>(new AzureServiceBus(configuration["ServiceBusConnectionString"]));

            // ServiceBus (Sem envio pra fila)
            //AddDependencyInjection<IMediatorHandlerQueue>(new BusService());
        }

        public static InMemoryBusTest Instance()
        {
            if (instance == null)
                instance = new InMemoryBusTest();
            return instance;
        }
    }
}
