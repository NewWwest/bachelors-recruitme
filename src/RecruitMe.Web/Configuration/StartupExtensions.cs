using Microsoft.Extensions.DependencyInjection;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Account.Commands;
using RecruitMe.Logic.Operations.Account.Helpers;
using RecruitMe.Logic.Operations.Account.Queries;
using RecruitMe.Logic.Operations.Account.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Web.Configuration
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
