using Microsoft.Extensions.DependencyInjection;
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
    }
}
