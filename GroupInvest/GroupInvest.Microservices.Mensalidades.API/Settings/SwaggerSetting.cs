using GroupInvest.Microservices.Mensalidades.API.Helpers;
using GroupInvest.Microservices.Mensalidades.API.Helpers.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupInvest.Microservices.Mensalidades.API.Settings
{
    public static class SwaggerSetting
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.ResolveConflictingActions(apiDescription => apiDescription.First()); // resolve o conflito de rotas iguais para versões da API

                // versões disponíveis para a API
                config.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "GroupInvest Mensalidades - versão 1.0", Version = "v1.0" });
                //config.SwaggerDoc("v2.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "GroupInvest Mensalidades - versão 2.0", Version = "v2.0" });

                // configura a versão selecionada para a url da chamada dos serviços
                config.OperationFilter<ApiVersionOperationFilter>();
                config.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                // Criação da lista de versões na tela do Swagger
                config.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();

                    if (actionApiVersionModel == null)
                        return true;

                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                        return actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == docName);

                    return actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == docName);
                });

                UserSecretsHelper.Load();
                config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{UserSecretsHelper.UrlAuthority}/connect/authorize"),
                            TokenUrl = new Uri($"{UserSecretsHelper.UrlAuthority}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { UserSecretsHelper.APIName, "API de Mensalidades" }
                            }
                        }
                    }
                });
                config.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }

        public static IApplicationBuilder ConfigureSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Mensalidades API v1.0");

                // adiciona outras versões da API para a seleção na tela do swagger
                //config.SwaggerEndpoint("/swagger/v2.0/swagger.json", "Mensalidades API v2.0");

                // joga direto na página do swagger
                config.RoutePrefix = string.Empty;

                // Dados para a autenticação no IdentityServer
                config.OAuthClientId(UserSecretsHelper.SwaggerUIClientId);
                config.OAuthAppName(UserSecretsHelper.APIName);
                config.OAuthUsePkce();
            });

            return app;
        }
    }
}
