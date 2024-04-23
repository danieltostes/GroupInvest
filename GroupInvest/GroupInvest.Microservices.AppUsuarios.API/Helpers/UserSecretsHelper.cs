using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.AppUsuarios.API.Helpers
{
    public class UserSecretsHelper
    {
        private static bool loaded;

        public static string MongoDBConnectionString { get; set; }
        public static string Database { get; set; }
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

                MongoDBConnectionString = configuration["MongoDbConnectionString"];
                Database = configuration["Database"];
                APIName = configuration["APIName"];
                SwaggerUIClientId = configuration["SwaggerUIClientId"];
                UrlAuthority = configuration["UrlAuthority"];

                loaded = true;
            }
        }
    }
}
