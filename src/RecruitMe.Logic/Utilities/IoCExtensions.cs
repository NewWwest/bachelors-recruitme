using Microsoft.Extensions.DependencyInjection;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var type = typeof(IAutoComponent);
            foreach (Type item in type.Assembly.GetTypes().Where(p => type.IsAssignableFrom(p)))
            {
                if (!item.IsAbstract && !item.IsInterface)
                {
                    container.AddTransient(item);
                }
            }
        }
    }
}
