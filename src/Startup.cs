using GitUpdater.Commands;
using GitUpdater.Helpers;
using GitUpdater.Wrapper;
using Microsoft.Extensions.DependencyInjection;

namespace GitUpdater
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGitChecker, GitChecker>();
            services.AddSingleton<IRepoFinder, RepoFinder>();

            services.AddSingleton<CheckCommand>();
        }
    }
}
