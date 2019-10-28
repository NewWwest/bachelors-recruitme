using Microsoft.Extensions.DependencyInjection;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Account;
using RecruitMe.Logic.Operations.Account.Helpers;
using RecruitMe.Logic.Operations.Account.Login;
using RecruitMe.Logic.Operations.Account.Registration;
using RecruitMe.Logic.Operations.Account.RemindLogin;
using RecruitMe.Logic.Operations.Account.ResetPassword;
using RecruitMe.Logic.Operations.Account.SetNewPassword;
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
            services.AddTransient<ILogger,ConsoleLogger>();
            services.AddTransient<BaseDbContext, ApplicationDbContext>();

            //Email
            services.AddTransient<SendEmailCommand>();

            //Account
            services.AddTransient<PasswordHasher>();
            services.AddTransient<GetUserQuery>();

            services.AddTransient<LoginUserQuery>();
            services.AddTransient<LoginRequestValidator>();

            services.AddTransient<ConfirmEmailCommand>();
            services.AddTransient<RegisterUserCommand>();
            services.AddTransient<RegisterRequestValidator>();

            services.AddTransient<ResetPasswordCommand>();

            services.AddTransient<SetNewPasswordValidator>();
            services.AddTransient<SetNewPasswordCommand>();

            services.AddTransient<RemindLoginCommand>();
            services.AddTransient<RemindLoginValidator>();

            //Recrutiment
            services.AddTransient<AddOrUpdatePersonalDataCommandRequestValidator>();
            services.AddTransient<GetPersonalDataQuery>();
            services.AddTransient<AddOrUpdatePersonalDataCommand>(); 



        }
    }
}
