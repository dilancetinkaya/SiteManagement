using Microsoft.AspNetCore.Mvc;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatController : ControllerBase
    {
        private readonly IFlatService _flatService;

        public FlatController(IFlatService flatService)
        {
            _flatService = flatService;
        }

        [HttpGet("List")]
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

        [HttpGet("Relation")]
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

        [HttpPost("Multiple")]
        public async Task<IActionResult> AddFlatMultiple(ICollection<CreateFlatDto> flats)
        {
            await _flatService.AddRangeAsync(flats);
            return Ok(flats);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFlat(UpdateFlatDto flat, int id)
        {
            _flatService.Update(flat, id);
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
