using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using SiteManagement.Service.Services;
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

        [HttpPost]
        public async Task<IActionResult> AddFlat(CreateFlatDto flat)
        {
            await _flatService.AddAsync(flat);
            return Ok(flat);
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
