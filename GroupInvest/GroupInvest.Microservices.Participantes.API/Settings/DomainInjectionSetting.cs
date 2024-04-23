using AutoMapper;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Infrastructure.Messaging;
using GroupInvest.Microservices.Participantes.API.Helpers;
using GroupInvest.Microservices.Participantes.Application;
using GroupInvest.Microservices.Participantes.Application.CommandHandlers;
using GroupInvest.Microservices.Participantes.Application.Commands.Participantes;
using GroupInvest.Microservices.Participantes.Application.Commands.Periodos;
using GroupInvest.Microservices.Participantes.Application.EventHandlers;
using GroupInvest.Microservices.Participantes.Application.Events.Participantes;
using GroupInvest.Microservices.Participantes.Application.Events.Periodos;
using GroupInvest.Microservices.Participantes.Application.Interfaces;
using GroupInvest.Microservices.Participantes.Application.Models.Dtos;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Participantes.Domain.Servicos;
using GroupInvest.Microservices.Participantes.Infra.DataAccess.Contextos;
using GroupInvest.Microservices.Participantes.Infra.DataAccess.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GroupInvest.Microservices.Participantes.API.Settings
{
    #region AutoMapper
    public class AutoMapperConfig
    {
        private static IMapper mapper;
        public static IMapper Mapper()
        {
            if (mapper == null)
            {
                var config = new MapperConfiguration(c =>
                {
                    c.CreateMap<IncluirParticipanteCommand, Participante>();
                    c.CreateMap<AlterarParticipanteCommand, Participante>()
                        .ForMember(p => p.Id, cmd => cmd.MapFrom(c => c.ParticipanteId))
                        .ForMember(p => p.UsuarioAplicativoId, cmd => cmd.MapFrom(c => string.IsNullOrEmpty(c.UsuarioAplicativoId) ? default : new Guid(c.UsuarioAplicativoId)));
                    c.CreateMap<Participante, ParticipanteIncluidoEvent>();
                    c.CreateMap<Participante, ParticipanteAlteradoEvent>();
                    c.CreateMap<Participante, ParticipanteDto>()
                        .ForMember(dto => dto.UsuarioAplicativoId, part => part.MapFrom(p => p.UsuarioAplicativoId == null ? null : p.UsuarioAplicativoId.ToString()));

                    
                    c.CreateMap<IncluirPeriodoCommand, Periodo>();
                    c.CreateMap<Periodo, PeriodoIncluidoEvent>();
                    c.CreateMap<Periodo, PeriodoDto>();

                    c.CreateMap<Adesao, AdesaoRealizadaEvent>();
                });
                mapper = config.CreateMapper();
            }
            return mapper;
        }
    }
    #endregion

    #region Bus (IMediatorHandler)
    public class InMemoryBusAPI : InMemoryBus
    {
        protected override void ConfigureHandlers()
        {
            UserSecretsHelper.Load();

            // DbContext
            AddDbContext<MicroserviceParticipantesDbContext>("GroupInvest.Microservices.Participantes", UserSecretsHelper.ParticipantesDbConnectionString);

            // CommandHandlers
            SetCommandHandler<IncluirParticipanteCommand, ParticipanteCommandHandler>();
            SetCommandHandler<AlterarParticipanteCommand, ParticipanteCommandHandler>();
            SetCommandHandler<RealizarAdesaoParticipanteCommand, ParticipanteCommandHandler>();

            SetCommandHandler<IncluirPeriodoCommand, PeriodoCommandHandler>();

            // EventHandlers
            SetEventHandler<ParticipanteIncluidoEvent, ParticipanteEventHandler>();
            SetEventHandler<ParticipanteAlteradoEvent, ParticipanteEventHandler>();
            SetEventHandler<AdesaoRealizadaEvent, ParticipanteEventHandler>();

            SetEventHandler<PeriodoIncluidoEvent, PeriodoEventHandler>();

            // Injeção de dependência
            AddDependencyInjection<IServicoParticipante, ServicoParticipante>();
            AddDependencyInjection<IServicoPeriodo, ServicoPeriodo>();
            AddDependencyInjection<IRepositorioParticipante, RepositorioParticipante>();
            AddDependencyInjection<IRepositorioPeriodo, RepositorioPeriodo>();
            AddDependencyInjection<IRepositorioAdesao, RepositorioAdesao>();

            // ServiceBus
            AddDependencyInjection<IMediatorHandlerQueue>(new AzureServiceBus(UserSecretsHelper.ServiceBusConnectionString));

            // AutoMapper
            AddDependencyInjection<IMapper>(AutoMapperConfig.Mapper());
        }
    }
    #endregion

    public static class DomainInjectionSetting
    {
        public static IServiceCollection ConfigureDomainInjection(this IServiceCollection services)
        {
            var bus = new InMemoryBusAPI();

            services.AddSingleton<IMediatorHandler>(obj => bus);
            services.AddSingleton(obj => AutoMapperConfig.Mapper());
            services.AddSingleton(obj => bus.GetDbContext(typeof(Startup).FullName));

            services.AddScoped<IParticipanteApi, ParticipanteApi>();
            services.AddScoped<IPeriodoApi, PeriodoApi>();
            services.AddScoped<IServicoParticipante, ServicoParticipante>();
            services.AddScoped<IServicoPeriodo, ServicoPeriodo>();
            services.AddScoped<IRepositorioParticipante, RepositorioParticipante>();
            services.AddScoped<IRepositorioPeriodo, RepositorioPeriodo>();
            services.AddScoped<IRepositorioAdesao, RepositorioAdesao>();

            return services;
        }
    }
}
