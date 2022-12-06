using SiteManagement.Infrastructure.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.IServices
{
    public interface IBuildingService
    {
        Task<BuildingDto> GetByIdAsync(int id);

        Task<ICollection<BuildingDto>> GetAllAsync();

        Task AddAsync(CreateBuildingDto buildingDto);

        Task RemoveAsync(int id);

        UpdateBuildingDto Update(UpdateBuildingDto buildingDto, int id);
    }
}
