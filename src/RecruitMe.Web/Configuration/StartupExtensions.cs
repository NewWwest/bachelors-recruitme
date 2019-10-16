using Microsoft.Extensions.DependencyInjection;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Account.Commands;
using RecruitMe.Logic.Operations.Account.Helpers;
using RecruitMe.Logic.Operations.Account.Queries;
using RecruitMe.Logic.Operations.Account.Validators;
using RecruitMe.Logic.Operations.Recruitment.Command;
using RecruitMe.Logic.Operations.Recruitment.Queries;
using RecruitMe.Logic.Operations.Recruitment.Validators;
using RecruitMe.Web.Services.Data;
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
            services.AddTransient<BaseDbContext, ApplicationDbContext>();

            services.AddTransient<RegisterUserCommand>();
            services.AddTransient<PasswordHasher>();
            services.AddTransient<LoginUserQuery>();
            services.AddTransient<LoginRequestValidator>();
            services.AddTransient<RegisterRequestValidator>();
            services.AddTransient<GetUserQuery>(); 
            services.AddTransient<GetUserFromClaimsQuery>();

            services.AddTransient<AddOrUpdatePersonalDataCommandRequestValidator>();
            services.AddTransient<GetPersonalDataQuery>();
            services.AddTransient<AddOrUpdatePersonalDataCommand>(); 



        }
    }
}
