using SiteManagement.Infrastructure.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.IServices
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(string id);
        Task<ICollection<UserDto>> GetAllAsync();
        Task AddAsync(CreateUserDto userDto);
        Task RemoveAsync(string id);
        Task<UpdateUserDto> UpdateAsync(UpdateUserDto userDto, string id);
        public Task<string> LoginAsync(LoginUserDto user);
        public Task Logout();
    }
}
