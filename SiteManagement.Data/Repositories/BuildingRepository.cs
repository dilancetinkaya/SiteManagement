using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Context;

namespace SiteManagement.Infrastructure.Repositories
{
    public class BuildingRepository : Repository<Building>, IBuildingRepository
    {
        public BuildingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
