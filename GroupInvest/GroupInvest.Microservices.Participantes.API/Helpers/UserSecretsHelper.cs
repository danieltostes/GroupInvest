using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.Participantes.API.Helpers
{
    public class UserSecretsHelper
    {
        private static bool loaded;

        public static string ParticipantesDbConnectionString { get; set; }
        public static string ServiceBusConnectionString { get; set; }
        public static string APIName { get; set; }
        public static string APIClientId { get; set; }
        public static string APIClientSecret { get; set; }
        public static string SenhaPadraoUsuarioAplicativo { get; set; }
        public static string SwaggerUIClientId { get; set; }
        public static string UrlAuthority { get; set; }
        public static string UrlGetToken { get; set; }
        public static string UrlRegisterAppUser { get; set; }

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

                ParticipantesDbConnectionString = configuration["ParticipantesDbConnectionString"];
                ServiceBusConnectionString = configuration["ServiceBusConnectionString"];

                APIName = configuration["APIName"];
                APIClientId = configuration["APIClientId"];
                APIClientSecret = configuration["APIClientSecret"];
                SenhaPadraoUsuarioAplicativo = configuration["SenhaPadraoUsuarioAplicativo"];

                SwaggerUIClientId = configuration["SwaggerUIClientId"];
                UrlAuthority = configuration["UrlAuthority"];
                UrlGetToken = configuration["UrlGetToken"];
                UrlRegisterAppUser = configuration["UrlRegisterAppUser"];

                loaded = true;
            }
        }
    }
}
