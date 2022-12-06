using SiteManagement.Infrastructure.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.IServices
{
    public interface IExpenseTypeService
    {
        Task<ExpenseTypeDto> GetByIdAsync(int id);

        Task<ICollection<ExpenseTypeDto>> GetAllAsync();

        Task AddAsync(CreateExpenseTypeDto expenseTypeDto);

        Task RemoveAsync(int id);

        UpdateExpenseTypeDto Update(UpdateExpenseTypeDto expenseTypeDto, int id);
    }
}
