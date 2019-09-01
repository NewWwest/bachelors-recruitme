using Microsoft.Extensions.DependencyInjection;
using RecrutMe.Logic.Logging;
using RecrutMe.Logic.Operations.Account.Commands;
using RecrutMe.Logic.Operations.Account.Helpers;
using RecrutMe.Logic.Operations.Account.Queries;
using RecrutMe.Logic.Operations.Account.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecrutMe.Web.Configuration
{
    public static class StartupExtensions
    {
        public static void AddDependencInjection(this IServiceCollection services)
        {
            //TODO: create a automatic injector for everything inheriting from BaseOperation and BaseValidator
            services.AddTransient<ILogger,ConsoleLogger>();

            services.AddTransient<RegisterUserCommand>();
            services.AddTransient<JwtTokenHelper>();
            services.AddTransient<LoginUserQuery>();
            services.AddTransient<LoginRequestValidator>();
            services.AddTransient<RegisterRequestValidator>();

        }
    }
}
