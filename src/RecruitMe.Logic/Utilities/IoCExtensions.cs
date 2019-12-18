using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RecruitMe.Logic.Utilities
{
    public static class IoCExtensions
    {
        public static T Get<T>(this IServiceProvider container)
        {
            return (T)container.GetService(typeof(T));
        }

        public static void RegisterAutoComponents(this IServiceCollection container)
        {
            Type type = typeof(IAutoComponent);
            foreach (Type item in type.Assembly.GetTypes().Where(p => type.IsAssignableFrom(p)))
            {
                if (!item.IsAbstract && !item.IsInterface)
                {
                    container.AddTransient(item);
                }
            }
        }

        public static T AddSingletonConfiguration<T>(this IServiceCollection container, IConfiguration configuration)
        {
            Type type = typeof(T);
            IConfigurationSection config = configuration.GetSection(type.Name);
            T instance = (T)Activator.CreateInstance(type);

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.PropertyType == typeof(DateTime))
                {
                    property.SetValue(instance, DateTime.Parse(config[property.Name]));
                }
                else
                {
                    property.SetValue(instance, config[property.Name]);
                }
            }

            container.AddSingleton(type, instance);
            return instance;
        }
    }
}
