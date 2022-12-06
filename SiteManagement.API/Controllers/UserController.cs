using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System.Threading.Tasks;

namespace SiteManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("List")]
        public async Task<IActionResult> GetUser()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);

        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserDto user,string password)
        {
            await _userService.AddAsync(user,password);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto user, string id)
        {
            await _userService.UpdateAsync(user, id);
            return Ok();


        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.RemoveAsync(id);
            return Ok();
        }
    }
}
