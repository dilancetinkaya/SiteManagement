using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.API.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
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

        [HttpGet("Relation")]
        public async Task<IActionResult> GetExpensesWithRelations()
        {
            var expense = await _expenseService.GetExpensesWithRelations();
            return Ok(expense);
        }

        [Authorize(Roles = "User", AuthenticationSchemes = "Bearer")]
        [HttpGet("User{id}")]
        public async Task<IActionResult> GetExpensesWithUserId(string id)
        {
            var expense = await _expenseService.GetExpensesWithUserIdAsync(id);
            return Ok(expense);
        }

        [HttpGet("Debttt")]
        public async Task<IActionResult> GetMonthlyDebt(DateTime startDate, DateTime endDate)
        {
            var expense = await _expenseService.GetDebtWithDate(startDate, endDate);
            return Ok(expense);
        }

        [HttpPost]
        public async Task<IActionResult> AddExpense(CreateExpenseDto expense)
        {
            await _expenseService.AddAsync(expense);
            return Ok(expense);
        }

        [HttpPost("Multiple")]
        public async Task<IActionResult> AddExpenseMultiple(ICollection<CreateExpenseDto> expenses)
        {
            await _expenseService.AddRangeAsync(expenses);
            return Ok(expenses);
        }

        [HttpPost("MultipleDebt")]
        public async Task<IActionResult> AddDebtMultiple(DebtMultipleDto expense)
        {
            await _expenseService.AddDebtMultiple(expense);
            return Ok(expense);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpenseAsync(UpdateExpenseDto expense, int id)
        {
            await _expenseService.UpdateAsync(expense, id);
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
