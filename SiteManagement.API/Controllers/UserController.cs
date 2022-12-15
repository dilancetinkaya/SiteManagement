using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("Login")]

        [AllowAnonymous]
        public IActionResult Login(UserDto user, string password)
        {
            var loginUser = _userService.Login(user, password);
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
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(CreateUserDto user)
        {
            await _userService.Register(user);
            return Ok();
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
