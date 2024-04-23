using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Versioning;
using System;
using System.Linq;

namespace GroupInvest.Microservices.Mensalidades.API.Helpers.Swagger
{
    public static class ActionDescriptorExtensions
    {
        // Classe para permitir a seleção da versão da API no uso do Swagger
        public static ApiVersionModel GetApiVersion(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor?.Properties
                .Where((kvp) => ((Type)kvp.Key).Equals(typeof(ApiVersionModel)))
                .Select(kvp => kvp.Value as ApiVersionModel).FirstOrDefault();
        }
    }
}
