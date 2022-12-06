using SiteManagement.Infrastructure.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.IServices
{
    public interface IExpenseService
    {
        Task<ExpenseDto> GetByIdAsync(int id);

        Task<ICollection<ExpenseDto>> GetAllAsync();

        Task AddAsync(CreateExpenseDto expenseDto);

        Task RemoveAsync(int id);

        UpdateExpenseDto Update(UpdateExpenseDto expenseDto, int id);
    }
}
