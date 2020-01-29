using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RecruitMe.Web;
using System;

namespace RecruitMe.Logic.Tests.IntegrationTests
{
    public class IocConfigurationTest
    {

        [Test]
        public void AllDependenciesPresentAndAccountedFor()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var startup = new Startup(config);
            var serviceCollection = new ServiceCollection();

            startup.ConfigureServices(serviceCollection);
            ServiceProvider provider = serviceCollection.BuildServiceProvider();
            foreach (ServiceDescriptor serviceDescriptor in serviceCollection)
            {
                Type serviceType = serviceDescriptor.ServiceType;
                
                if (serviceType.FullName.Contains("RecruitMe"))
                {
                    Assert.NotNull(provider.GetService(serviceType));
                }
            }
        }
    }
}


