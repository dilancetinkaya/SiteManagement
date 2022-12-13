using SiteManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Domain.IRepositories
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        Task<ICollection<Expense>> GetExpensesWithRelations();
        Task<ICollection<Expense>> GetExpensesWithUserIdAsync(string id);
    }
}
