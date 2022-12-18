using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.API.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetBuilding()
        {
            var buildings = await _buildingService.GetAllAsync();
            return Ok(buildings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuildingById(int id)
        {
            var building = await _buildingService.GetByIdAsync(id);
            return Ok(building);
        }

        [HttpPost]
        public async Task<IActionResult> AddBuilding(CreateBuildingDto building)
        {
            await _buildingService.AddAsync(building);
            return Ok(building);
        }

        [HttpPost("Bulk")]
        public async Task<IActionResult> AddBuildingMultiple(ICollection<CreateBuildingDto> buildings)
        {
            await _buildingService.AddRangeAsync(buildings);
            return Ok(buildings);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBuildingAsync(UpdateBuildingDto building, int id)
        {
            await _buildingService.UpdateAsync(building, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(int id)
        {
            await _buildingService.RemoveAsync(id);
            return Ok();
        }
    }
}
