using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.Mensalidades.API.Settings
{
    public static class ApplicationCookieSetting
    {
        public static IServiceCollection ConfigureApplicationCookie(this IServiceCollection services)
        {
            // Configuração para retornar o erro correto "Não Autorizado" quando usuário tenta acessar um recurso da api sem se autenticar
            services.ConfigureApplicationCookie(op =>
            {
                op.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            return services;
        }
    }
}
