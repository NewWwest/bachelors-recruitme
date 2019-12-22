using System.Collections.Generic;
using System.Reflection;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Utilities;
using RecruitMe.Web.Configuration;
using RecruitMe.Web.Services;
using RecruitMe.Web.Services.Data;

namespace RecruitMe.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            BusinessConfiguration config = services.AddSingletonConfiguration<BusinessConfiguration>(Configuration);

            services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
                optionsBuilder.UseMySql(Configuration["ConnectionString"])
            );
            services.AddCors();
            services.AddMvc();
            services.AddMvcCore().AddAuthorization();

            services.AddIdentityServer(options =>
                    options.IssuerUri = config.BaseAddress
                )
                .AddDeveloperSigningCredential(true, "IdentityServer.rsa.json")
                .AddInMemoryIdentityResources(ISConfig.GetIdentityResources())
                .AddInMemoryApiResources(ISConfig.GetApiResources())
                .AddInMemoryClients(ISConfig.GetClients())
                .AddProfileService<CustomProfileService>()
                .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
                .AddJwtBearerClientAuthentication();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = config.BaseAddress;
                    options.ApiName = ISConfig.AuthScope;
                    options.RequireHttpsMetadata = false;
                    options.SupportedTokens = SupportedTokens.Jwt;
                });


            services.AddDependencyInjection();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext, EndpointConfig endpointConfig)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            List<string> origins = new List<string> { endpointConfig.DotpayBaseAddress };

            app.UseCors(
                options => options.WithOrigins(origins.ToArray()).AllowAnyMethod().WithHeaders("authorization", "accept", "content-type", "origin")
            );

            app.UseIdentityServer();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            app.UseStaticFiles();

            dbContext.EnsureCreated();
        }
    }
}
