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
using RecruitMe.Logic.Operations.Payments.Payment;
using RecruitMe.Logic.Operations.Payments.PaymentLink;
using RecruitMe.Logic.Operations.Recruitment.ProfileData;
using RecruitMe.Logic.Operations.Recruitment.ProfileFiles;
using RecruitMe.Web.Services.Data;

namespace RecruitMe.Web.Configuration
{
    public static class StartupExtensions
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<ILogger,ConsoleLogger>();
            services.AddTransient<BaseDbContext, ApplicationDbContext>();


            services.AddTransient<IFileSaver, LocalFileStorage>();
            services.AddTransient<IPictureSaver, LocalFileStorage>();
            services.AddTransient<IFileRepository, LocalFileStorage>();

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
            services.AddTransient<SaveFileCommand>();
            services.AddTransient<DeleteFileCommand>();

            services.AddTransient<GetFileQuery>();

            //Payments
            services.AddTransient<CreatePaymentLinkCommand>();
            services.AddTransient<GetNewPaymentDescriptionQuery>();
            services.AddTransient<GetExistingPaymentLinkQuery>();
            services.AddTransient<RemoveExistingPaymentLink>();
        }
    }
}
