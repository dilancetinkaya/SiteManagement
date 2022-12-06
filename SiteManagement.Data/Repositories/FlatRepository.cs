using SiteManagement.Infrastructure.Context;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Repositories;

namespace SiteManagement.Data.Repositories
{
    public class FlatRepository : Repository<Flat>, IFlatRepository
    {
        public FlatRepository(AppDbContext context) : base(context)
        {
        }
    }
}
