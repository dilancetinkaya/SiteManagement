using SiteManagement.Infrastructure.Context;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;

namespace SiteManagement.Infrastructure.Repositories
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(AppDbContext context) : base(context)
        {
        }
    }
}
