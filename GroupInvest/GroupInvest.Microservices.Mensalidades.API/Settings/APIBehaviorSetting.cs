using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GroupInvest.Microservices.Mensalidades.API.Settings
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
