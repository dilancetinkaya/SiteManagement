using SiteManagement.Infrastructure.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.IServices
{
    public interface IBlockService
    {
        Task<BlockDto> GetByIdAsync(int id);
        Task<ICollection<BlockDto>> GetAllAsync();
        Task AddAsync(CreateBlockDto blockDto);
        Task<ICollection<CreateBlockDto>> AddRangeAsync(ICollection<CreateBlockDto> blockDtos);
        Task RemoveAsync(int id);
        UpdateBlockDto Update(UpdateBlockDto blockDto, int id);
    }
}
