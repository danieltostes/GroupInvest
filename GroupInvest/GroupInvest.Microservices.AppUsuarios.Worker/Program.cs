using AutoMapper;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Infrastructure.Messaging;
using GroupInvest.Microservices.AppUsuarios.Application.CommandHandlers;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Emprestimos;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Mensalidades;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Pagamentos;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.RendimentoParcial;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Transacoes;
using GroupInvest.Microservices.AppUsuarios.Application.EventHandlers;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Emprestimos;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Mensalidades;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Pagamentos;
using GroupInvest.Microservices.AppUsuarios.Application.Events.RendimentoParcial;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Transacoes;
using GroupInvest.Microservices.AppUsuarios.Application.Models.VO;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.AppUsuarios.Domain.Servicos;
using GroupInvest.Microservices.AppUsuarios.Infra.DataAccess.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.AppUsuarios.Worker
{
    class Program
    {
        #region AutoMapper
        private class AutoMapperConfig
        {
            private static IMapper mapper;
            public static IMapper Mapper()
            {
                if (mapper == null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<IntegracaoMensalidadesGeradasEvent, RegistrarMensalidadesCommand>();
                        cfg.CreateMap<IntegracaoEmprestimoConcedidoEvent, RegistrarEmprestimoConcedidoCommand>();
                        cfg.CreateMap<IntegracaoPagamentoRealizadoEvent, RegistrarPagamentoCommand>();
                        cfg.CreateMap<IntegracaoTransacaoRealizadaEvent, TransacaoVO>();
                    });
                    mapper = config.CreateMapper();
                }
                return mapper;
            }
        }
        #endregion

        #region Configuração do Bus
        private class InMemoryBusWorker : InMemoryBus
        {
            protected override void ConfigureHandlers()
            {
                // User secrets
                var userSecretsBuilder = new ConfigurationBuilder();
                userSecretsBuilder.AddUserSecrets<Program>();
                var configuration = userSecretsBuilder.Build();

                // MongoDb
                MongoClient client = new MongoClient(configuration["MongoDbConnectionString"]);
                IMongoDatabase db = client.GetDatabase(configuration["Database"]);
                AddDependencyInjection<IMongoDatabase>(db);

                // CommandHandlers
                SetCommandHandler<RegistrarMensalidadesCommand, MensalidadeCommandHandler>();
                SetCommandHandler<RegistrarEmprestimoConcedidoCommand, EmprestimoCommandHandler>();
                SetCommandHandler<RegistrarAtualizacaoSaldoEmprestimosCommand, EmprestimoCommandHandler>();
                SetCommandHandler<RegistrarPagamentoCommand, PagamentoCommandHandler>();
                SetCommandHandler<RegistrarTransacaoCommand, TransacaoCommandHandler>();
                SetCommandHandler<RegistrarRendimentoParcialCommand, RendimentoParcialCommandHandler>();

                // EventHandlers
                SetEventHandler<IntegracaoMensalidadesGeradasEvent, MensalidadeEventHandler>();
                SetEventHandler<MensalidadesRegistradasEvent, MensalidadeEventHandler>();
                SetEventHandler<IntegracaoEmprestimoConcedidoEvent, EmprestimoEventHandler>();
                SetEventHandler<IntegracaoEmprestimoAtualizacaoSaldoEvent, EmprestimoEventHandler>();
                SetEventHandler<EmprestimoConcedidoRegistradoEvent, EmprestimoEventHandler>();
                SetEventHandler<SaldosEmprestimosAtualizadosEvent, EmprestimoEventHandler>();
                SetEventHandler<IntegracaoPagamentoRealizadoEvent, PagamentoEventHandler>();
                SetEventHandler<PagamentoRealizadoRegistradoEvent, PagamentoEventHandler>();
                SetEventHandler<IntegracaoTransacaoRealizadaEvent, TransacaoEventHandler>();
                SetEventHandler<TransacaoRegistradaEvent, TransacaoEventHandler>();
                SetEventHandler<IntegracaoRendimentoParcialEvent, RendimentoParcialEventHandler>();
                SetEventHandler<RendimentoParcialRegistradoEvent, RendimentoParcialEventHandler>();

                // Injeção de dependência
                AddDependencyInjection<IRepositorioMensalidade, RepositorioMensalidade>();
                AddDependencyInjection<IRepositorioEmprestimo, RepositorioEmprestimo>();
                AddDependencyInjection<IRepositorioTransacao, RepositorioTransacao>();
                AddDependencyInjection<IRepositorioRendimentoParcialPeriodo, RepositorioRendimentoParcialPeriodo>();

                AddDependencyInjection<IServicoMensalidade, ServicoMensalidade>();
                AddDependencyInjection<IServicoEmprestimo, ServicoEmprestimo>();
                AddDependencyInjection<IServicoTransacao, ServicoTransacao>();
                AddDependencyInjection<IServicoRendimentoParcialPeriodo, ServicoRendimentoParcialPeriodo>();

                // AutoMapper
                AddDependencyInjection<IMapper>(AutoMapperConfig.Mapper());
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
                Console.WriteLine("Worker Aplicativo Ativo...");
                await host.RunAsync();
            }
        }
    }
}
