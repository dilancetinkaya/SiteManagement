using SiteManagement.Domain.PaymentApiModel;
using SiteManagement.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.IServices
{
    public interface IExpenseService
    {
        Task<ExpenseDto> GetByIdAsync(int id);
        Task SendMail();
        Task<ICollection<ExpenseDto>> GetAllAsync();
        Task<ICollection<ExpenseDto>> GetExpensesWithRelations();
        Task<ICollection<ExpenseDto>> GetExpensesWithUserIdAsync(string id);
        Task<ICollection<ExpenseDto>> GetDebtWithDate(DateTime startDate, DateTime endDate);
        Task AddDebtMultiple(DebtMultipleDto expenseDto);
        Task AddAsync(CreateExpenseDto expenseDto);
        Task<CreatePaymentDto> AddPayment(CreatePaymentDto createPaymentDto);
        Task<ICollection<CreateExpenseDto>> AddRangeAsync(ICollection<CreateExpenseDto> expenseDtos);
        Task RemoveAsync(int id);
        UpdateExpenseDto Update(UpdateExpenseDto expenseDto, int id);
    }
}
