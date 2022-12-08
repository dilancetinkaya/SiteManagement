using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SiteManagement.Domain.Entities;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userManager.CreateAsync(user, userDto.Password);

        }

        public async Task<ICollection<UserDto>> GetAllAsync()
        {
            var users = _userManager.Users;
            var usersDto = _mapper.Map<ICollection<UserDto>>(users);
            return usersDto;
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task RemoveAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<UpdateUserDto> UpdateAsync(UpdateUserDto userDto, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.Id = id;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.CarLicensePlate = userDto.CarLicensePlate;
            user.IdentificationNumber = userDto.IdentificationNumber;
            user.PhoneNumber = userDto.PhoneNumber;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            await _userManager.UpdateAsync(user);
            return userDto;

        }
    }
}
