using SiteManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Domain.IRepositories
{
    public interface IFlatRepository : IRepository<Flat>
    {
        Task<ICollection<Flat>> GetAllFlatsByRelations();
    }
}
