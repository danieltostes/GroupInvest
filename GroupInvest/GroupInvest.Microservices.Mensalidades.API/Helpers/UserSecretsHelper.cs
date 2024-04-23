using Microsoft.Extensions.Configuration;

namespace GroupInvest.Microservices.Mensalidades.API.Helpers
{
    public class UserSecretsHelper
    {
        private static bool loaded;

        public static string MensalidadesDbConnectionString { get; set; }
        public static string ServiceBusConnectionString { get; set; }
        public static string APIName { get; set; }
        public static string SwaggerUIClientId { get; set; }
        public static string UrlAuthority { get; set; }

        public static void Load(bool development = false)
        {
            if (!loaded)
            {
                // User secrets
                var userSecretsBuilder = new ConfigurationBuilder();
                userSecretsBuilder.AddJsonFile("appsettings.json");
                if (development)
                {
                    userSecretsBuilder.AddUserSecrets<Startup>();
                }
                var configuration = userSecretsBuilder.Build();

                MensalidadesDbConnectionString = configuration["MensalidadesDbConnectionString"];
                ServiceBusConnectionString = configuration["ServiceBusConnectionString"];
                APIName = configuration["APIName"];
                SwaggerUIClientId = configuration["SwaggerUIClientId"];
                UrlAuthority = configuration["UrlAuthority"];

                loaded = true;
            }
        }
    }
}
