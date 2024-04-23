using GroupInvest.Microservices.AppUsuarios.API.Helpers;
using GroupInvest.Microservices.AppUsuarios.Application;
using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.APIs;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.AppUsuarios.Domain.Servicos;
using GroupInvest.Microservices.AppUsuarios.Infra.DataAccess.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.AppUsuarios.API.Settings
{
    public static class DomainInjectionSetting
    {
        public static IServiceCollection ConfigureDomainInjection(this IServiceCollection services)
        {
            UserSecretsHelper.Load();
            
            services.AddSingleton(db =>
            {
                MongoClient client = new MongoClient(UserSecretsHelper.MongoDBConnectionString);
                IMongoDatabase database = client.GetDatabase(UserSecretsHelper.Database);
                return database;
            });

            services.AddScoped<IRepositorioRendimentoParcialPeriodo, RepositorioRendimentoParcialPeriodo>();
            services.AddScoped<IRepositorioMensalidade, RepositorioMensalidade>();
            services.AddScoped<IRepositorioEmprestimo, RepositorioEmprestimo>();
            services.AddScoped<IRepositorioTransacao, RepositorioTransacao>();
            
            services.AddScoped<IServicoRendimentoParcialPeriodo, ServicoRendimentoParcialPeriodo>();
            services.AddScoped<IServicoMensalidade, ServicoMensalidade>();
            services.AddScoped<IServicoEmprestimo, ServicoEmprestimo>();
            services.AddScoped<IServicoTransacao, ServicoTransacao>();

            services.AddScoped<IMensalidadeApi, MensalidadeApi>();
            services.AddScoped<IEmprestimoApi, EmprestimoApi>();
            services.AddScoped<IDashboardApi, DashboardApi>();

            return services;
        }
    }
}
