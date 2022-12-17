using Microsoft.EntityFrameworkCore;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.Repositories
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<ICollection<Expense>> GetExpensesWithRelations()
        {
            return await _context.Expenses
                .Include(x => x.ExpenseType)
                .Include(x => x.Flat)
                .ThenInclude(x => x.User).ToListAsync();
        }

        /// <summary>
        /// Kullanıcı id ye göre giderleri ve detaylarını getirir
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ICollection<Expense>> GetExpensesWithUserIdAsync(string userId)
        {

            return await _context.Expenses.Where(x => x.Flat.UserId == userId)
                .Include(x => x.ExpenseType)
               .Include(x => x.Flat)
               .ThenInclude(x => x.User).ToListAsync();
        }

    }
}
