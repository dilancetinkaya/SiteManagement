using AutoMapper;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Service.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private IFlatRepository _flatRepository;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper, IFlatRepository flatRepository)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
            _flatRepository = flatRepository;
        }

        public async Task AddAsync(CreateExpenseDto expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);
            await _expenseRepository.AddAsync(expense);
        }

        public async Task<ICollection<CreateExpenseDto>> AddRangeAsync(ICollection<CreateExpenseDto> expenseDtos)
        {
            var expenses = _mapper.Map<ICollection<Expense>>(expenseDtos);
            await _expenseRepository.AddRangeAsync(expenses);
            return expenseDtos;
        }

        public async Task<ICollection<ExpenseDto>> GetAllAsync()
        {
            var expenses = await _expenseRepository.GetAllAsync();
            return _mapper.Map<ICollection<ExpenseDto>>(expenses);
        }

        public async Task<ExpenseDto> GetByIdAsync(int id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense is null) throw new Exception("Expense is not found");

            var expenseDto = _mapper.Map<ExpenseDto>(expense);
            return expenseDto;
        }

        public async Task RemoveAsync(int id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense is null) throw new Exception("Expense is not found");

            _expenseRepository.Remove(expense);
        }

        public UpdateExpenseDto Update(UpdateExpenseDto expenseDto, int id)
        {
            var updatedExpense = _mapper.Map<Expense>(expenseDto);
            updatedExpense.Id = id;
            _expenseRepository.Update(updatedExpense);
            return expenseDto;
        }
        public async Task<ICollection<ExpenseDto>> GetExpensesWithRelations()
        {
            var expenses = await _expenseRepository.GetExpensesWithRelations();
            var expenseDtos = expenses.Select(e => new ExpenseDto()
            {
                Id = e.Id,
                IsPaid = e.IsPaid,
                Price = e.Price,
                InvoiceDate = e.InvoiceDate,
                ExpenseTypeId = e.ExpenseTypeId,
                FlatId = e.FlatId,

            }).ToList();
            return expenseDtos;
        }

        public async Task AddDebtMultiple(CreateExpenseDto expenseDto)
        {
            var flats = _mapper.Map<ICollection<FlatDto>>(await _flatRepository.GetAllAsync());
            var expenseDtoList = flats.Select(f => new CreateExpenseDto()
            {
                FlatId = f.Id,
                InvoiceDate = DateTime.Now,
                Price = expenseDto.Price,
                ExpenseTypeId = expenseDto.ExpenseTypeId,
                IsPaid = false
            }).ToList();
            await AddRangeAsync(expenseDtoList);
        }
        public async Task SendMail()
        {
            var expenses = await _expenseRepository.GetAllAsync();
            foreach (var expense in expenses)
            {
                if (!expense.IsPaid)
                {
                    var email = new Message
                    {
                        MessageContent = $" {expense}",
                        ReceiverId = expense.Flat.UserId,
                        //SenderId=
                    };
                }

            }

        }
    }
}
