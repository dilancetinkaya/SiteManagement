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
    public class ExpenseTypeController : ControllerBase
    {
        private readonly IExpenseTypeService _expenseTypeService;

        public ExpenseTypeController(IExpenseTypeService expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetExpenseType()
        {
            var expenseTypes = await _expenseTypeService.GetAllAsync();
            return Ok(expenseTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseTypeById(int id)
        {
            var expenseType = await _expenseTypeService.GetByIdAsync(id);
            return Ok(expenseType);
        }

        [HttpPost]
        public async Task<IActionResult> AddExpenseType(CreateExpenseTypeDto expenseType)
        {
            await _expenseTypeService.AddAsync(expenseType);
            return Ok(expenseType);
        }

        [HttpPost("Multiple")]
        public async Task<IActionResult> AddExpenseTypeMultiple(ICollection<CreateExpenseTypeDto> expenseTypes)
        {
            await _expenseTypeService.AddRangeAsync(expenseTypes);
            return Ok(expenseTypes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpenseTypeAsync(UpdateExpenseTypeDto expenseType, int id)
        {
            await _expenseTypeService.Update(expenseType, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseType(int id)
        {
            await _expenseTypeService.RemoveAsync(id);
            return Ok();
        }
    }
}
