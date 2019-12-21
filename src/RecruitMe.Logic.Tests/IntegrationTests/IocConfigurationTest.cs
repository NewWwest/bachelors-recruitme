using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;
using RecruitMe.Web;
using System;
using System.Collections.Generic;

namespace RecruitMe.Logic.Tests.IntegrationTests
{
    public class IocConfigurationTest
    {

        [Test]
        public void AllDependenciesPresentAndAccountedFor()
        {
            var startup = new Startup(new FakeConfiguration());
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

        class FakeConfiguration : IConfiguration
        {
            public string this[string key] {
                get => throw new NotImplementedException(); 
                set => throw new NotImplementedException(); 
            }

            public IEnumerable<IConfigurationSection> GetChildren()
            {
                throw new NotImplementedException();
            }

            public IChangeToken GetReloadToken()
            {
                throw new NotImplementedException();
            }

            public IConfigurationSection GetSection(string key)
            {
                throw new NotImplementedException();
            }
        }
    }
}


