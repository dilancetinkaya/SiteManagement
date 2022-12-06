using SiteManagement.Infrastructure.Context;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;

namespace SiteManagement.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
