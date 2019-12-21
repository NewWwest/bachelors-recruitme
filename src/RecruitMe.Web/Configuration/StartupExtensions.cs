using Microsoft.Extensions.DependencyInjection;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Utilities;
using RecruitMe.Web.Services.Data;

namespace RecruitMe.Web.Configuration
{
    public static class StartupExtensions
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<ILogger, ConsoleLogger>();
            services.AddTransient<BaseDbContext, ApplicationDbContext>();

            services.AddTransient<IFileSaver, LocalFileStorage>();
            services.AddTransient<IPictureSaver, LocalFileStorage>();
            services.AddTransient<IFileRepository, LocalFileStorage>();

            services.RegisterAutoComponents();
        }
    }
}
