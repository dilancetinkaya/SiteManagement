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
        Task<ICollection<CreateExpenseTypeDto>> AddRangeAsync(ICollection<CreateExpenseTypeDto> expenseTypeDtos);
        Task RemoveAsync(int id);
        Task Update(UpdateExpenseTypeDto expenseTypeDto, int id);
    }
}
