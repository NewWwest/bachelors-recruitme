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
using RecruitMe.Logic.Operations.Recruitment.ProfileData;
using RecruitMe.Logic.Operations.Recruitment.ProfileFiles;
using RecruitMe.Web.Services.Data;

namespace RecruitMe.Web.Configuration
{
    public static class StartupExtensions
    {
        public static void AddDependencInjection(this IServiceCollection services)
        {
            services.AddTransient<ILogger,ConsoleLogger>();
            services.AddTransient<BaseDbContext, ApplicationDbContext>();
            services.AddTransient<IFileStorage, LocalFileStorage>();

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
            services.AddTransient<AddOrUpdateProfileDataCommandRequestValidator>();
            services.AddTransient<GetProfileDataQuery>();
            services.AddTransient<AddOrUpdateProfileDataCommand>();

            services.AddTransient<SetNewProfilePictureCommand>();

            services.AddTransient<GetFileQuery>();
        }
    }
}
