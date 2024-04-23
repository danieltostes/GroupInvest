using GroupInvest.Microservices.AppUsuarios.API.Helpers;
using GroupInvest.Microservices.AppUsuarios.API.Settings;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GroupInvest.Microservices.AppUsuarios.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            UserSecretsHelper.Load(HostEnvironment.IsDevelopment());

            services.ConfigureAPIBehavior();
            services.ConfigureDomainInjection();
            services.ConfigureControllers();
            services.ConfigureAPIVersioning();
            services.ConfigureSwagger();
            services.ConfigureApplicationCookie();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    UserSecretsHelper.Load();

                    options.Authority = UserSecretsHelper.UrlAuthority;
                    options.RequireHttpsMetadata = false;
                    options.ApiName = UserSecretsHelper.APIName;
                });

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("Admin",
                //    policy => policy.RequireRole(new string[] { "Admin" }));

                options.AddPolicy("Admin",
                    policy => policy.RequireAssertion(context =>
                        context.User.HasClaim(c =>
                            (c.Type == "role" && c.Value == "Admin") ||
                            (c.Type == "role" && c.Value == "AppUser") ||
                            (c.Type == "client_role" && c.Value == "Admin")
                    )));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.ConfigureSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
