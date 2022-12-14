using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.API.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
    [Route("api/flats")]
    [ApiController]
    public class FlatController : ControllerBase
    {
        private readonly IFlatService _flatService;

        public FlatController(IFlatService flatService)
        {
            _flatService = flatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlat()
        {
            var flats = await _flatService.GetAllAsync();
            return Ok(flats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlatById(int id)
        {
            var flat = await _flatService.GetByIdAsync(id);
            return Ok(flat);
        }

        [HttpGet("relation")]
        public async Task<IActionResult> GetFlatByRelations()
        {
            var flats = await _flatService.GetAllFlatsByRelations();
            return Ok(flats);
        }

        [HttpPost]
        public async Task<IActionResult> AddFlat(CreateFlatDto flat)
        {
            await _flatService.AddAsync(flat);
            return Ok(flat);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddFlatMultiple(ICollection<CreateFlatDto> flats)
        {
            await _flatService.AddRangeAsync(flats);
            return Ok(flats);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlatAsync(UpdateFlatDto flat, int id)
        {
            await _flatService.UpdateAsync(flat, id);
            return Ok();
        }
        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUserFlatAsync(UpdateFlatUserDto flat, int id)
        {
            await _flatService.UpdateFlatUserAsync(flat, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlat(int id)
        {
            await _flatService.RemoveAsync(id);
            return Ok();
        }
    }
}
