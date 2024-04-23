using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.AppUsuarios.API.Settings
{
    public static class ControllerSetting
    {
        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.ReturnHttpNotAcceptable = true;
            })
            .AddNewtonsoftJson(op =>
            {
                // configuração para ignorar no json de retorno da api referências circulares
                op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            return services;
        }
    }
}
