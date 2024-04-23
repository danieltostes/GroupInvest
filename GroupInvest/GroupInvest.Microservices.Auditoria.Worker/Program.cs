using AutoMapper;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Infrastructure.Messaging;
using GroupInvest.Microservices.Auditoria.Application.CommandHandlers;
using GroupInvest.Microservices.Auditoria.Application.Commands.AuditoriaBase;
using GroupInvest.Microservices.Auditoria.Application.EventHandlers;
using GroupInvest.Microservices.Auditoria.Application.Events;
using GroupInvest.Microservices.Auditoria.Domain.Entidades;
using GroupInvest.Microservices.Auditoria.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Auditoria.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Auditoria.Domain.Servicos;
using GroupInvest.Microservices.Auditoria.Infra.DataAccess.Contextos;
using GroupInvest.Microservices.Auditoria.Infra.DataAccess.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.Auditoria.Worker
{
    class Program
    {
        #region Configuração do Bus
        private class InMemoryBusWorker : InMemoryBus
        {
            protected override void ConfigureHandlers()
            {
                // User secrets
                var userSecretsBuilder = new ConfigurationBuilder();
                userSecretsBuilder.AddUserSecrets<Program>();
                var configuration = userSecretsBuilder.Build();

                // DbContexts
                AddDbContext<MicroserviceAuditoriaDbContext>("GroupInvest.Microservices.Auditoria", configuration["AuditoriaDbConnectionString"]);

                // CommandHandlers
                SetCommandHandler<IncluirAuditoriaBaseCommand, AuditoriaBaseCommandHandler>();

                // EventHandlers
                SetEventHandler<AlteracaoDadosEvent, AuditoriaBaseEventHandler>();

                // Injeção de dependência
                AddDependencyInjection<IServicoAuditoria, ServicoAuditoria>();
                AddDependencyInjection<IRepositorioAuditoria, RepositorioAuditoria>();

                // AutoMapper
                AddDependencyInjection<IMapper>(AutoMapperConfig.Mapper());
            }
        }
        #endregion

        #region Configuração do AutoMapper
        private class AutoMapperConfig
        {
            private static IMapper mapper;
            public static IMapper Mapper()
            {
                if (mapper == null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<IncluirAuditoriaBaseCommand, AuditoriaBase>();
                    });
                    mapper = config.CreateMapper();
                }
                return mapper;
            }
        }
        #endregion

        static async Task Main()
        {
            // User secrets
            var userSecretsBuilder = new ConfigurationBuilder();
            userSecretsBuilder.AddUserSecrets<Program>();
            var configuration = userSecretsBuilder.Build();

            var builder = new HostBuilder();
            builder.ConfigureWebJobs(b =>
            {
                b.AddServiceBus(sbOptions =>
                {
                    sbOptions.ConnectionString = configuration["ServiceBusConnectionString"];
                    sbOptions.MessageHandlerOptions.MaxConcurrentCalls = 1;
                    sbOptions.MessageHandlerOptions.AutoComplete = true;
                });
            }).ConfigureServices(services =>
            {
                services.AddSingleton<IMediatorHandler>(s => new InMemoryBusWorker());
            });
            var host = builder.Build();
            using (host)
            {
                Console.WriteLine("Worker Auditoria Ativo...");
                await host.RunAsync();
            }
        }
    }
}
