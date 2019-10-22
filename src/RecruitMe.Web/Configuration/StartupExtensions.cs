using Microsoft.Extensions.DependencyInjection;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Account;
using RecruitMe.Logic.Operations.Account.Helpers;
using RecruitMe.Logic.Operations.Account.Login;
using RecruitMe.Logic.Operations.Account.Registration;
using RecruitMe.Logic.Operations.Email;
using RecruitMe.Logic.Operations.Recruitment.Command;
using RecruitMe.Logic.Operations.Recruitment.Queries;
using RecruitMe.Logic.Operations.Recruitment.Validators;
using RecruitMe.Web.Services.Data;

namespace RecruitMe.Web.Configuration
{
    public static class StartupExtensions
    {
        public static void AddDependencInjection(this IServiceCollection services)
        {
            //TODO: create a automatic injector for everything inheriting from BaseOperation and BaseValidator
            services.AddTransient<ILogger,ConsoleLogger>();
            services.AddTransient<BaseDbContext, ApplicationDbContext>();

            services.AddTransient<SendEmailCommand>();

            services.AddTransient<RegisterUserCommand>();
            services.AddTransient<ConfirmEmailCommand>();
            services.AddTransient<PasswordHasher>();
            services.AddTransient<LoginUserQuery>();
            services.AddTransient<LoginRequestValidator>();
            services.AddTransient<RegisterRequestValidator>();
            services.AddTransient<GetUserQuery>(); 

            services.AddTransient<AddOrUpdatePersonalDataCommandRequestValidator>();
            services.AddTransient<GetPersonalDataQuery>();
            services.AddTransient<AddOrUpdatePersonalDataCommand>(); 



        }
    }
}
