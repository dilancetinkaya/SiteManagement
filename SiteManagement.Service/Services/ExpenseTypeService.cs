using AutoMapper;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using SiteManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Service.Services
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IMapper _mapper;

        public ExpenseTypeService(IExpenseTypeRepository expenseTypeRepository, IMapper mapper)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateExpenseTypeDto expenseTypeDto)
        {
            var expenseType = _mapper.Map<ExpenseType>(expenseTypeDto);
            await _expenseTypeRepository.AddAsync(expenseType);
        }

        public async Task<ICollection<CreateExpenseTypeDto>> AddRangeAsync(ICollection<CreateExpenseTypeDto> expenseTypeDtos)
        {
            var expenseTypes = _mapper.Map<ICollection<ExpenseType>>(expenseTypeDtos);
            await _expenseTypeRepository.AddRangeAsync(expenseTypes);
            return expenseTypeDtos;
        }

        public async Task<ICollection<ExpenseTypeDto>> GetAllAsync()
        {
            var expenseTypes = await _expenseTypeRepository.GetAllAsync();
            return _mapper.Map<ICollection<ExpenseTypeDto>>(expenseTypes);
        }

        public async Task<ExpenseTypeDto> GetByIdAsync(int id)
        {
            var expenseType = await _expenseTypeRepository.GetByIdAsync(id);
            if (expenseType is null) throw new Exception("ExpenseType is not found");

            return _mapper.Map<ExpenseTypeDto>(expenseType);
        }

        public async Task RemoveAsync(int id)
        {
            var expenseType = await _expenseTypeRepository.GetByIdAsync(id);
            if (expenseType is null) throw new Exception("ExpenseType is not found");

             _expenseTypeRepository.Remove(expenseType);
        }

        public UpdateExpenseTypeDto Update(UpdateExpenseTypeDto expenseTypeDto, int id)
        {
            var updatedExpenseType = _mapper.Map<ExpenseType>(expenseTypeDto);
            updatedExpenseType.Id = id;
            _expenseTypeRepository.Update(updatedExpenseType);
            return expenseTypeDto;
        }
    }
}