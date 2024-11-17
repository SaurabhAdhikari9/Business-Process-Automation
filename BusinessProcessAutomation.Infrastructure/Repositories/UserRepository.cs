using BusinessProcessAutomation.Application.Interface.IRepositories;
using BusinessProcessAutomation.Domain.Entities;
using BusinessProcessAutomation.Infrastructure.DbContext;

namespace BusinessProcessAutomation.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context):base(context)
        {
                
        }
    }
}
