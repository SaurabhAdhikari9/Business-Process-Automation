using BusinessProcessAutomation.Application.Interface.IRepositories;
using BusinessProcessAutomation.Application.Interface.IServices;
using BusinessProcessAutomation.Infrastructure.Repositories;
using BusinessProcessAutomation.Infrastructure.Services;

namespace BusinessProcessAutomation.Extensions
{
    public static class DependencyInjections
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            #region Repository Injection
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUserRepository,  IUserRepository>();
            #endregion

            #region Service Injection
            services.AddTransient<IUserService, UserService>();
            #endregion
        }
    }
}
