using AutoMapper;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Infrastructure.Messaging;
using GroupInvest.Microservices.Mensalidades.Application.CommandHandlers;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Adesoes;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Mensalidades;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Participantes;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Periodos;
using GroupInvest.Microservices.Mensalidades.Application.EventHandlers;
using GroupInvest.Microservices.Mensalidades.Application.Events.Adesoes;
using GroupInvest.Microservices.Mensalidades.Application.Events.Mensalidades;
using GroupInvest.Microservices.Mensalidades.Application.Models.VO;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Mensalidades.Domain.Servicos;
using GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Contextos;
using GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.Mensalidades.Worker
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
                AddDbContext<MicroserviceMensalidadesDbContext>("GroupInvest.Microservices.Mensalidades", configuration["MensalidadesDbConnectionString"]);

                // CommandHandlers
                SetCommandHandler<IncluirPeriodoCommand, PeriodoCommandHandler>();
                SetCommandHandler<IncluirParticipanteCommand, ParticipanteCommandHandler>();
                SetCommandHandler<RealizarAdesaoCommand, AdesaoCommandHandler>();
                SetCommandHandler<GerarMensalidadesCommand, MensalidadeCommandHandler>();
                
                // EventHandlers
                SetEventHandler<IntegracaoAdesaoRealizadaEvent, AdesaoEventHandler>();
                SetEventHandler<AdesaoRealizadaEvent, AdesaoEventHandler>();

                // Injeção de dependência
                AddDependencyInjection<IServicoParticipante, ServicoParticipante>();
                AddDependencyInjection<IServicoPeriodo, ServicoPeriodo>();
                AddDependencyInjection<IServicoMensalidade, ServicoMensalidade>();
                AddDependencyInjection<IServicoAdesao, ServicoAdesao>();
                AddDependencyInjection<IServicoEmprestimo, ServicoEmprestimo>();
                AddDependencyInjection<IServicoPagamento, ServicoPagamento>();

                AddDependencyInjection<IRepositorioParticipante, RepositorioParticipante>();
                AddDependencyInjection<IRepositorioPeriodo, RepositorioPeriodo>();
                AddDependencyInjection<IRepositorioMensalidade, RepositorioMensalidade>();
                AddDependencyInjection<IRepositorioAdesao, RepositorioAdesao>();
                AddDependencyInjection<IRepositorioEmprestimo, RepositorioEmprestimo>();
                AddDependencyInjection<IRepositorioPrevisaoPagamentoEmprestimo, RepositorioPrevisaoPagamentoEmprestimo>();
                AddDependencyInjection<IRepositorioPagamentoParcialEmprestimo, RepositorioPagamentoParcialEmprestimo>();
                AddDependencyInjection<IRepositorioPagamento, RepositorioPagamento>();
                AddDependencyInjection<IRepositorioItemPagamento, RepositorioItemPagamento>();
                AddDependencyInjection<IRepositorioDistribuicaoCotas, RepositorioDistribuicaoCotas>();
                AddDependencyInjection<IRepositorioDistribuicaoParticipante, RepositorioDistribuicaoParticipante>();

                // AutoMapper
                AddDependencyInjection<IMapper>(AutoMapperConfig.Mapper());

                // IMediatorHandlerQueue
                AddDependencyInjection<IMediatorHandlerQueue>(new AzureServiceBus(configuration["ServiceBusConnectionString"]));
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
                        cfg.CreateMap<IntegracaoAdesaoRealizadaEvent, IncluirPeriodoCommand>()
                            .ForMember(cmd => cmd.PeriodoId, eve => eve.MapFrom(ev => ev.Periodo.Id))
                            .ForMember(cmd => cmd.AnoReferencia, eve => eve.MapFrom(ev => ev.Periodo.AnoReferencia))
                            .ForMember(cmd => cmd.DataInicioPeriodo, eve => eve.MapFrom(ev => ev.Periodo.DataInicio))
                            .ForMember(cmd => cmd.DataTerminoPeriodo, eve => eve.MapFrom(ev => ev.Periodo.DataTermino))
                            .ForMember(cmd => cmd.Ativo, eve => eve.MapFrom(ev => ev.Periodo.Ativo));

                        cfg.CreateMap<IntegracaoAdesaoRealizadaEvent, IncluirParticipanteCommand>()
                            .ForMember(cmd => cmd.ParticipanteId, eve => eve.MapFrom(ev => ev.Participante.Id))
                            .ForMember(cmd => cmd.Nome, eve => eve.MapFrom(ev => ev.Participante.Nome));

                        cfg.CreateMap<IncluirPeriodoCommand, Periodo>()
                            .ForMember(p => p.Id, cmd => cmd.MapFrom(c => c.PeriodoId))
                            .ForMember(p => p.AnoReferencia, cmd => cmd.MapFrom(c => c.AnoReferencia))
                            .ForMember(p => p.DataInicioPeriodo, cmd => cmd.MapFrom(c => c.DataInicioPeriodo))
                            .ForMember(p => p.DataTerminoPeriodo, cmd => cmd.MapFrom(c => c.DataTerminoPeriodo));

                        cfg.CreateMap<IncluirParticipanteCommand, Participante>()
                            .ForMember(p => p.Id, cmd => cmd.MapFrom(c => c.ParticipanteId))
                            .ForMember(p => p.Nome, cmd => cmd.MapFrom(c => c.Nome));

                        cfg.CreateMap<Mensalidade, MensalidadeVO>();
                    });
                    mapper = config.CreateMapper();
                }
                return mapper;
            }
        }
        #endregion

        static async Task Main(string[] args)
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
                Console.WriteLine("Worker Mensalidades Ativo...");
                await host.RunAsync();
            }
        }
    }
}
