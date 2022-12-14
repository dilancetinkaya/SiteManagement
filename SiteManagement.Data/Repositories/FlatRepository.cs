using Microsoft.EntityFrameworkCore;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Context;
using SiteManagement.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Data.Repositories
{
    public class FlatRepository : Repository<Flat>, IFlatRepository
    {
        public FlatRepository(AppDbContext context) : base(context)
        {


        }
        public async Task<ICollection<Flat>> GetAllFlatsByRelations()
        {
            return await _context.Flats
                .Include(x => x.User)
                .Include(x => x.Building)
                .OrderBy(x => x.FlatNumber)
                .ToListAsync();
        }
    }
}
