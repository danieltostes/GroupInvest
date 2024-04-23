using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GroupInvest.Microservices.Mensalidades.API.Settings
{
    public static class APIVersionSetting
    {
        public static IServiceCollection ConfigureAPIVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.ReportApiVersions = true;
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.DefaultApiVersion = new ApiVersion(1, 0);
            });

            return services;
        }
    }
}
