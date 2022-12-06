using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Context;

namespace SiteManagement.Infrastructure.Repositories
{
    public class ExpenseTypeRepository : Repository<ExpenseType>, IExpenseTypeRepository
    {
        public ExpenseTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
