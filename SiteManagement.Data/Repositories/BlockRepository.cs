using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Context;

namespace SiteManagement.Infrastructure.Repositories
{
    public class BlockRepository : Repository<Block>, IBlockRepository
    {
        public BlockRepository(AppDbContext context) : base(context)
        {

        }
    }
}
