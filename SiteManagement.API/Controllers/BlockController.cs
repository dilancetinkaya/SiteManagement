using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.API.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
    [Route("api/blocks")]
    [ApiController]
    public class BlockController : ControllerBase
    {
        private readonly IBlockService _blockService;

        public BlockController(IBlockService blockService)
        {
            _blockService = blockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlock()
        {
            var blockList = await _blockService.GetAllAsync();
            return Ok(blockList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlockById(int id)
        {
            var block = await _blockService.GetByIdAsync(id);
            return Ok(block);
        }

        [HttpPost]
        public async Task<IActionResult> AddBlock(CreateBlockDto block)
        {
            await _blockService.AddAsync(block);
            return Ok(block);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddBlockMultiple(ICollection<CreateBlockDto> blocks)
        {
            await _blockService.AddRangeAsync(blocks);
            return Ok(blocks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlockAsync(UpdateBlockDto block, int id)
        {
            await _blockService.UpdateAsync(block, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlock(int id)
        {
            await _blockService.RemoveAsync(id);
            return Ok();
        }

    }
}
