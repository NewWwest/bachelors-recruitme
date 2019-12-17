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
using RecruitMe.Logic.Operations.Administration.Candidate;
using RecruitMe.Logic.Operations.Administration.Exam;
using RecruitMe.Logic.Operations.Administration.ExamCategory;
using RecruitMe.Logic.Operations.Administration.Teacher;
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
            services.AddTransient<ILogger, ConsoleLogger>();
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

            //admin panel
            services.AddTransient<AddExamCategoryCommand>();
            services.AddTransient<DeleteExamCategoryCommand>();
            services.AddTransient<GetExamCategoriesQuery>();
            services.AddTransient<AddExamCategoryValidator>();
            services.AddTransient<UpdateExamCategoryValidator>();
            services.AddTransient<UpdateExamCategoryCommand>();

            services.AddTransient<AddTeacherCommand>();
            services.AddTransient<DeleteTeacherCommand>();
            services.AddTransient<GetTeachersQuery>();
            services.AddTransient<AddTeacherValidator>();
            services.AddTransient<UpdateTeacherValidator>();
            services.AddTransient<UpdateTeacherCommand>();

            services.AddTransient<AddExamCommand>();
            services.AddTransient<DeleteExamCommand>();
            services.AddTransient<GetExamsQuery>();
            services.AddTransient<AddExamValidator>();
            services.AddTransient<UpdateExamValidator>();
            services.AddTransient<UpdateExamCommand>();
            services.AddTransient<GetExamDetailsQuery>(); 

            services.AddTransient<GetCandidatesQuery>();
            services.AddTransient<UpdateCandidateCommand>();
            services.AddTransient<DeleteCandidateCommand>(); 
            services.AddTransient<GetEnrolledExamsQuerry>();
            services.AddTransient<AddOrUpdateExamTakerCommand>();
            services.AddTransient<DeleteExamTakerCommand>();
        }
    }
}
