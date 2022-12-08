using AutoMapper;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Service.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateExpenseDto expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);
            await _expenseRepository.AddAsync(expense);
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

        public UpdateExpenseDto Update(UpdateExpenseDto expenseDto,int id)
        {
            var updatedExpense = _mapper.Map<Expense>(expenseDto);
            updatedExpense.Id = id;
            _expenseRepository.Update(updatedExpense);
            return expenseDto;
        }
    }
}
