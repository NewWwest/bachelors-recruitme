using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
                optionsBuilder.UseMySql(Configuration["ConnectionString"])
            );
            services.AddCors();
            services.AddMvc();
            services.AddMvcCore().AddAuthorization();

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            if (bool.TrueString == Configuration["UseHttpsRedirection"])
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    options.HttpsPort = int.Parse(Configuration["https_port"]);
                });
            }


            services.AddIdentityServer(options => options.IssuerUri = config.BaseAddress)
                .AddSigningCredential(new X509Certificate2(Configuration["SslCertificate"], Configuration["SslCertificatePassword"]))
                .AddValidationKey(new X509Certificate2(Configuration["SslCertificate"], Configuration["SslCertificatePassword"]))
                .AddInMemoryIdentityResources(ISConfig.GetIdentityResources())
                .AddInMemoryApiResources(ISConfig.GetApiResources())
                .AddInMemoryClients(ISConfig.GetClients())
                .AddProfileService<CustomProfileService>()
                .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
                .AddJwtBearerClientAuthentication();


            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = config.BaseAddressNoSsl;
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
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }

            if (bool.TrueString == Configuration["ShowErrorDetails"])
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHsts();
            if (bool.TrueString == Configuration["UseHttpsRedirection"])
            {
                app.UseHttpsRedirection();
            }

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
