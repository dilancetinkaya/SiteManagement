using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;
        private const string AllUserKey = "USERALL";
        private MemoryCacheEntryOptions _cacheOptions;

        public UserService(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager,
                           IConfiguration configuration, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative: TimeSpan.FromMinutes(10));
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
            _memoryCache.Remove(AllUserKey);

        }

        public async Task<ICollection<UserDto>> GetAllAsync()
        {
            return await _memoryCache.GetOrCreateAsync(AllUserKey, async flatsCache =>
            {
                flatsCache.SetOptions(_cacheOptions);
                var users = await (_userManager.Users).ToListAsync();
                var usersDto = _mapper.Map<ICollection<UserDto>>(users);
                return usersDto;
            });
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
            _memoryCache.Remove(AllUserKey);
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
            _memoryCache.Remove(AllUserKey);
            return userDto;
        }

        /// <summary>
        /// Kullanici login islemi.Kullaniciya token uretilir.
        /// </summary>
        public async Task<string> LoginAsync(LoginUserDto user)
        {
            var signUser = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);

            if (signUser is null) return string.Empty;

            var userByEmnail = await _userManager.FindByEmailAsync(user.Email);
            var userRole = await _userManager.GetRolesAsync(userByEmnail);

            var token = GenerateJwt.GetJwtToken(user.Email, userRole.First(), _configuration["Jwt:Key"],
                       _configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                       TimeSpan.FromDays(Double.Parse(_configuration["Jwt:ExpirationInDays"]))).ToString();
            return token;
        }

        /// <summary>
        /// Kullanici cikis yapar
        /// </summary>
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
