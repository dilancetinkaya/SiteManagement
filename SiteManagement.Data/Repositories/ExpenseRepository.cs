using Microsoft.EntityFrameworkCore;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.Repositories
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<Expense>> GetExpensesWithRelations()
        {
            return await _context.Expenses
                .Include(x => x.ExpenseType)
                .Include(x => x.Flat)
                .ThenInclude(x => x.User).ToListAsync();
        }
    }
}
