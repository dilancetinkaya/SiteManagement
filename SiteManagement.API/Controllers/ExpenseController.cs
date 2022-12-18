using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System;
using System.Collections.Generic;
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

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("List")]
        public async Task<IActionResult> GetExpense()
        {
            var expenses = await _expenseService.GetAllAsync();
            return Ok(expenses);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            var expense = await _expenseService.GetByIdAsync(id);
            return Ok(expense);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("Relations")]
        public async Task<IActionResult> GetExpensesWithRelations()
        {
            var expense = await _expenseService.GetExpensesWithRelations();
            return Ok(expense);
        }

        [Authorize(Roles = "Admin,User", AuthenticationSchemes = "Bearer")]
        [HttpGet("Users/{id}")]
        public async Task<IActionResult> GetExpensesWithUserId(string id)
        {
            var expense = await _expenseService.GetExpensesWithUserIdAsync(id);
            return Ok(expense);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("Date")]
        public async Task<IActionResult> GetDebtWithDate(DateTime startDate, DateTime endDate)
        {
            var expense = await _expenseService.GetDebtWithDate(startDate, endDate);
            return Ok(expense);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> AddExpense(CreateExpenseDto expense)
        {
            await _expenseService.AddAsync(expense);
            return Ok(expense);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPost("Bulk")]
        public async Task<IActionResult> AddExpenseMultiple(ICollection<CreateExpenseDto> expenses)
        {
            await _expenseService.AddRangeAsync(expenses);
            return Ok(expenses);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPost("UsersDebt")]
        public async Task<IActionResult> AddDebtMultiple(DebtMultipleDto expense)
        {
            await _expenseService.AddDebtMultiple(expense);
            return Ok(expense);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpenseAsync(UpdateExpenseDto expense, int id)
        {
            await _expenseService.UpdateAsync(expense, id);
            return Ok();
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            await _expenseService.RemoveAsync(id);
            return Ok();
        }
    }
}
