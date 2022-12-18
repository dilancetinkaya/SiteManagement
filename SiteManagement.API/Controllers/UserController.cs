using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System.Threading.Tasks;

namespace SiteManagement.API.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Login")]

        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginUserDto user)
        {
            var loginUser = await _userService.LoginAsync(user);
            if (loginUser == null) return BadRequest();

            return Ok(loginUser);
        }
        [HttpGet("Logout")]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            _userService.Logout();
            return Ok();
        }


        [HttpGet]
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
        public async Task<IActionResult> AddUser(CreateUserDto user)
        {
            await _userService.AddAsync(user);
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
