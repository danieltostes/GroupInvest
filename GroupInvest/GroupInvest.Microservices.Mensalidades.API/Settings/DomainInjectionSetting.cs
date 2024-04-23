using AutoMapper;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Infrastructure.Messaging;
using GroupInvest.Microservices.Mensalidades.API.Helpers;
using GroupInvest.Microservices.Mensalidades.Application;
using GroupInvest.Microservices.Mensalidades.Application.CommandHandlers;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Emprestimos;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Pagamentos;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Periodos;
using GroupInvest.Microservices.Mensalidades.Application.EventHandlers;
using GroupInvest.Microservices.Mensalidades.Application.Events.Emprestimos;
using GroupInvest.Microservices.Mensalidades.Application.Events.Pagamentos;
using GroupInvest.Microservices.Mensalidades.Application.Events.Periodos;
using GroupInvest.Microservices.Mensalidades.Application.Interfaces;
using GroupInvest.Microservices.Mensalidades.Application.Models.Dtos;
using GroupInvest.Microservices.Mensalidades.Application.Models.VO;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Mensalidades.Domain.Servicos;
using GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Contextos;
using GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace GroupInvest.Microservices.Mensalidades.API.Settings
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
                    c.CreateMap<Emprestimo, EmprestimoDto>()
                        .ForMember(dto => dto.Id, emp => emp.MapFrom(e => e.Id))
                        .ForMember(dto => dto.DataEmprestimo, emp => emp.MapFrom(e => e.Data))
                        .ForMember(dto => dto.DataProximoPagamento, emp => emp.MapFrom(e => e.DataProximoVencimento))
                        .ForMember(dto => dto.ValorEmprestimo, emp => emp.MapFrom(e => e.Valor))
                        .ForMember(dto => dto.SaldoEmprestimo, emp => emp.MapFrom(e => e.Saldo))
                        .ForMember(dto => dto.StatusEmprestimo, emp => emp.MapFrom(e => e.Quitado ? "QUITADO" : "ATIVO"))
                        .ForPath(dto => dto.Participante.Id, emp => emp.MapFrom(e => e.Adesao.Participante.Id))
                        .ForPath(dto => dto.Participante.Nome, emp => emp.MapFrom(e => e.Adesao.Participante.Nome));

                    c.CreateMap<PrevisaoPagamentoEmprestimo, PrevisaoPagamentoEmprestimoDto>()
                        .ForMember(dto => dto.EmprestimoId, prev => prev.MapFrom(p => p.Emprestimo.Id));

                    c.CreateMap<Mensalidade, MensalidadeDto>()
                        .ForMember(dto => dto.AdesaoId, mens => mens.MapFrom(m => m.Adesao.Id));

                    c.CreateMap<Mensalidade, MensalidadeVO>();

                    c.CreateMap<ConcessaoEmprestimoDto, ConcederEmprestimoCommand>();

                    c.CreateMap<Emprestimo, EmprestimoVO>()
                        .ForMember(vo => vo.ParticipanteId, emp => emp.MapFrom(e => e.Adesao.Participante.Id));

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
            AddDbContext<MicroserviceMensalidadesDbContext>("GroupInvest.Microservices.Mensalidades", UserSecretsHelper.MensalidadesDbConnectionString);

            // CommandHandlers
            SetCommandHandler<ConcederEmprestimoCommand, EmprestimoCommandHandler>();
            SetCommandHandler<AtualizarPrevisoesPagamentoEmprestimosCommand, EmprestimoCommandHandler>();
            SetCommandHandler<RealizarPagamentoCommand, PagamentoCommandHandler>();
            SetCommandHandler<RealizarPagamentoRetroativoCommand, PagamentoCommandHandler>();
            SetCommandHandler<EncerrarPeriodoCommand, PeriodoCommandHandler>();
            SetCommandHandler<CalcularRendimentoParcialPeriodoCommand, PeriodoCommandHandler>();

            // EventHandlers
            SetEventHandler<EmprestimoConcedidoEvent, EmprestimoEventHandler>();
            SetEventHandler<PrevisoesPagamentoEmprestimosAtualizadasEvent, EmprestimoEventHandler>();
            SetEventHandler<PagamentoRealizadoEvent, PagamentoEventHandler>();
            SetEventHandler<PagamentoRetroativoRealizadoEvent, PagamentoEventHandler>();
            SetEventHandler<PeriodoEncerradoEvent, PeriodoEventHandler>();
            SetEventHandler<IntegracaoRendimentoParcialPeriodoEvent, PeriodoEventHandler>();

            // Injeção de dependência
            AddDependencyInjection<IServicoParticipante, ServicoParticipante>();
            AddDependencyInjection<IServicoPeriodo, ServicoPeriodo>();
            AddDependencyInjection<IServicoAdesao, ServicoAdesao>();
            AddDependencyInjection<IServicoMensalidade, ServicoMensalidade>();
            AddDependencyInjection<IServicoEmprestimo, ServicoEmprestimo>();
            AddDependencyInjection<IServicoPagamento, ServicoPagamento>();

            AddDependencyInjection<IRepositorioParticipante, RepositorioParticipante>();
            AddDependencyInjection<IRepositorioPeriodo, RepositorioPeriodo>();
            AddDependencyInjection<IRepositorioAdesao, RepositorioAdesao>();
            AddDependencyInjection<IRepositorioMensalidade, RepositorioMensalidade>();
            AddDependencyInjection<IRepositorioEmprestimo, RepositorioEmprestimo>();
            AddDependencyInjection<IRepositorioPrevisaoPagamentoEmprestimo, RepositorioPrevisaoPagamentoEmprestimo>();
            AddDependencyInjection<IRepositorioPagamentoParcialEmprestimo, RepositorioPagamentoParcialEmprestimo>();
            AddDependencyInjection<IRepositorioPagamento, RepositorioPagamento>();
            AddDependencyInjection<IRepositorioItemPagamento, RepositorioItemPagamento>();
            AddDependencyInjection<IRepositorioDistribuicaoCotas, RepositorioDistribuicaoCotas>();
            AddDependencyInjection<IRepositorioDistribuicaoParticipante, RepositorioDistribuicaoParticipante>();

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
            services.AddSingleton<IMediatorHandlerQueue>(new AzureServiceBus(UserSecretsHelper.ServiceBusConnectionString));
            services.AddSingleton(obj => AutoMapperConfig.Mapper());
            services.AddSingleton(obj => bus.GetDbContext(typeof(Startup).FullName));

            services.AddScoped<IServicoParticipante, ServicoParticipante>();
            services.AddScoped<IServicoPeriodo, ServicoPeriodo>();
            services.AddScoped<IServicoAdesao, ServicoAdesao>();
            services.AddScoped<IServicoMensalidade, ServicoMensalidade>();
            services.AddScoped<IServicoEmprestimo, ServicoEmprestimo>();
            services.AddScoped<IServicoPagamento, ServicoPagamento>();

            services.AddScoped<IRepositorioParticipante, RepositorioParticipante>();
            services.AddScoped<IRepositorioPeriodo, RepositorioPeriodo>();
            services.AddScoped<IRepositorioAdesao, RepositorioAdesao>();
            services.AddScoped<IRepositorioMensalidade, RepositorioMensalidade>();
            services.AddScoped<IRepositorioEmprestimo, RepositorioEmprestimo>();
            services.AddScoped<IRepositorioPrevisaoPagamentoEmprestimo, RepositorioPrevisaoPagamentoEmprestimo>();
            services.AddScoped<IRepositorioPagamentoParcialEmprestimo, RepositorioPagamentoParcialEmprestimo>();
            services.AddScoped<IRepositorioPagamento, RepositorioPagamento>();
            services.AddScoped<IRepositorioItemPagamento, RepositorioItemPagamento>();
            services.AddScoped<IRepositorioDistribuicaoCotas, RepositorioDistribuicaoCotas>();
            services.AddScoped<IRepositorioDistribuicaoParticipante, RepositorioDistribuicaoParticipante>();

            services.AddScoped<IEmprestimoApi, EmprestimoApi>();
            services.AddScoped<IMensalidadeApi, MensalidadeApi>();
            services.AddScoped<IPagamentoApi, PagamentoApi>();
            services.AddScoped<IPeriodoApi, PeriodoApi>();

            return services;
        }
    }
}
