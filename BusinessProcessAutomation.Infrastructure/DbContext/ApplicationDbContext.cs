using BusinessProcessAutomation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace BusinessProcessAutomation.Infrastructure.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
                
        }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}
