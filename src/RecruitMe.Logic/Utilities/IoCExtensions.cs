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

        private static IDictionary<Type, Func<string, object>> conversionDictionary =
            new Dictionary<Type, Func<string, object>>()
            {
                { typeof(bool), (b) => bool.Parse(b) },
                { typeof(DateTime), (d) => DateTime.Parse(d) },
                { typeof(decimal), (d) => decimal.Parse(d) },
                { typeof(float), (f) => float.Parse(f) },
                { typeof(int), (i) => int.Parse(i) },
                { typeof(string), (s) => s }
            };

        public static T AddSingletonConfiguration<T>(this IServiceCollection container, IConfiguration configuration)
        {
            Type type = typeof(T);
            IConfigurationSection config = configuration.GetSection(type.Name);
            T instance = (T)Activator.CreateInstance(type);

            foreach (PropertyInfo property in type.GetProperties())
            {
                object value = conversionDictionary[property.PropertyType](config[property.Name]);
                property.SetValue(instance, value);
            }

            container.AddSingleton(type, instance);
            return instance;
        }
    }
}
