using SiteManagement.Infrastructure.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.IServices
{
    public interface IExpenseService
    {
        Task<ExpenseDto> GetByIdAsync(int id);

        Task<ICollection<ExpenseDto>> GetAllAsync();
        Task<ICollection<ExpenseDto>> GetExpensesWithRelations();
        Task AddDebtMultiple(CreateExpenseDto expenseDto);

        Task AddAsync(CreateExpenseDto expenseDto);
        Task<ICollection<CreateExpenseDto>> AddRangeAsync(ICollection<CreateExpenseDto> expenseDtos);

        Task RemoveAsync(int id);

        UpdateExpenseDto Update(UpdateExpenseDto expenseDto, int id);
    }
}
