using BusinessProcessAutomation.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BusinessProcessAutomation.Extensions
{
    public static class DbConfigure
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseNpgsql(configuration.GetConnectionString("PostgreSqlConnection"), b => b.MigrationsAssembly("BusinessProcessAutomation.Infrastructure")));
        }
    }
}
