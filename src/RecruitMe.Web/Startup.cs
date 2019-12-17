using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data.Entities;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = "server=127.0.0.1;database=db1;user=app1;password=test-test-test";
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
                optionsBuilder.UseMySql(connectionString)
            );
            services.AddCors();
            services.AddMvc();
            services.AddMvcCore().AddAuthorization();

            services.AddIdentityServer(options =>
            {
                options.IssuerUri = "http://0.0.0.0:5000";
            })
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
                    options.Authority = EndpointConfig.BaseAddress;
                    options.ApiName = ISConfig.AuthScope;
                    options.RequireHttpsMetadata = false;
                    options.SupportedTokens = SupportedTokens.Jwt;
                });


            services.AddDependencInjection();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
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


            List<string> origins = new List<string> { "" };

            app.UseCors(
                options => options.WithOrigins(origins.ToArray()).AllowAnyMethod().WithHeaders("authorization", "accept", "content-type", "origin")
            );

            app.UseIdentityServer();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "areas",
                //    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                //);

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            app.UseStaticFiles();
        }
    }
}
