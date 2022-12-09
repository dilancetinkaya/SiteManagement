using SiteManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Domain.IRepositories
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        Task<List<Expense>> GetExpensesWithRelations();
    }
}
