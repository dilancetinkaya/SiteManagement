using SiteManagement.Infrastructure.Context;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SiteManagement.Data.Repositories
{
    public class FlatRepository : Repository<Flat>, IFlatRepository
    {
        public FlatRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<Flat>> GetAllFlatsByRelations()
        {
            return await _context.Flats
                .Include(x => x.User)
                .Include(x => x.Building)
                .OrderBy(x => x.FlatNumber)
                .ToListAsync();
        }
    }
}
