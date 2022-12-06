using Microsoft.AspNetCore.Mvc;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System.Threading.Tasks;

namespace SiteManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        [HttpGet("List")]
        public async Task<IActionResult> GetExpense()
        {
            var expenses = await _expenseService.GetAllAsync();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            var expense = await _expenseService.GetByIdAsync(id);
            return Ok(expense);
        }

        [HttpPost]
        public async Task<IActionResult> AddExpense(CreateExpenseDto expense)
        {
            await _expenseService.AddAsync(expense);
            return Ok(expense);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExpense(UpdateExpenseDto expense, int id)
        {
            _expenseService.Update(expense, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            await _expenseService.RemoveAsync(id);
            return Ok();
        }
    }
}
