using SiteManagement.Infrastructure.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.IServices
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(string id);
        Task<UserDto> GetByName(string name);

        Task<ICollection<UserDto>> GetAllAsync();

        Task AddAsync(CreateUserDto userDto);
       
        Task RemoveAsync(string id);

        Task<UpdateUserDto> UpdateAsync(UpdateUserDto userDto, string id);
       
    }
}
