using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SiteManagement.Application.Jwt;
using SiteManagement.Domain.Entities;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task AddAsync(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var addedUser = await _userManager.CreateAsync(user, userDto.Password);

            if (!addedUser.Succeeded) throw new Exception("User can not");

            await _userManager.AddToRoleAsync(user, "User");
        }

        public async Task<ICollection<UserDto>> GetAllAsync()
        {
            var users = await ( _userManager.Users).ToListAsync();
            var usersDto = _mapper.Map<ICollection<UserDto>>(users);
            return usersDto;
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) throw new Exception("User not found");

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
            await _userManager.UpdateAsync(user);
            return userDto;
        }

        public async Task Register(CreateUserDto user)
        {
            await _userManager.CreateAsync(
                          new User
                          {
                              Email = user.Email,
                              FirstName = user.FirstName,
                              LastName = user.LastName,
                              UserName = user.UserName,
                          },user.Password);

        }
        public string Login(UserDto user, string password)
        {
            _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            var token = GenerateJwt.GetJwtToken(user.UserName, _configuration["Jwt:Key"],
                       _configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], TimeSpan.FromDays(Double.Parse(_configuration["Jwt:ExpirationInDays"]))).ToString();
            return token;
        }

        public async Task Logout()
        {
           await _signInManager.SignOutAsync();
        }

 
    }
}
