using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RecruitMe.Logic.Configuration;

namespace RecruitMe.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .Build();

            CreateWebHostBuilder(args)
                .UseKestrel(opts =>
                {
                    opts.Listen(IPAddress.Any, int.Parse(config["https_port"]), listenOptions =>
                    {
                        listenOptions.UseHttps(
                            new X509Certificate2(config["SslCertificate"], config["SslCertificatePassword"])
                        );
                    });
                    opts.Listen(IPAddress.Any, int.Parse(config["http_port"]));
                })
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
