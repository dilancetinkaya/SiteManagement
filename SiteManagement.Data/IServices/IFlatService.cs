using SiteManagement.Infrastructure.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.IServices
{
    public interface IFlatService
    {
        Task<FlatDto> GetByIdAsync(int id);
        Task<ICollection<FlatDto>> GetAllAsync();
        Task AddAsync(CreateFlatDto flatDto);
        Task<ICollection<CreateFlatDto>> AddRangeAsync(ICollection<CreateFlatDto> flatDtos);
        Task<ICollection<FlatDto>> GetAllFlatsByRelations();
        Task RemoveAsync(int id);
        UpdateFlatDto Update(UpdateFlatDto flatDto, int id);
    }
}
