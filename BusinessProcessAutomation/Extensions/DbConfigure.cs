using BusinessProcessAutomation.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
namespace BusinessProcessAutomation.Extensions
{
    public static class DbConfigure
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseSqlite(configuration.GetConnectionString("db")));
        }
    }
}
