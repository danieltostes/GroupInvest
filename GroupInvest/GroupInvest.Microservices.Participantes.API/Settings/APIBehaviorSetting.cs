using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.Participantes.API.Settings
{
    public static class APIBehaviorSetting
    {
        public static IServiceCollection ConfigureAPIBehavior(this IServiceCollection services)
        {
            // Configuração para suprimir a validação do parâmetro recebido pelo controller antes de entrar no método
            services.Configure<ApiBehaviorOptions>(op =>
            {
                op.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}
