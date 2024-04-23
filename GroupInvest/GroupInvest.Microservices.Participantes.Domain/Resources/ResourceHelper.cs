using System.Resources;

namespace GroupInvest.Microservices.Participantes.Domain.Resources
{
    public static class ResourceHelper
    {
        private static ResourceManager resource = new ResourceManager(typeof(Mensagens));

        public static string Get(string resourceName)
        {
            return resource.GetString(resourceName);
        }
    }
}
