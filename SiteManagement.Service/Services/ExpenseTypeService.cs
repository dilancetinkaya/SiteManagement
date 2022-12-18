using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Service.Services
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private const string AllExpenseTypeKey = "EXPENSETYPEALL";
        private MemoryCacheEntryOptions _cacheOptions;

        public ExpenseTypeService(IExpenseTypeRepository expenseTypeRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative: TimeSpan.FromMinutes(10));
            _expenseTypeRepository = expenseTypeRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateExpenseTypeDto expenseTypeDto)
        {
            var expenseType = _mapper.Map<ExpenseType>(expenseTypeDto);

            await _expenseTypeRepository.AddAsync(expenseType);
            _memoryCache.Remove(AllExpenseTypeKey);
        }

        public async Task<ICollection<CreateExpenseTypeDto>> AddRangeAsync(ICollection<CreateExpenseTypeDto> expenseTypeDtos)
        {
            var expenseTypes = _mapper.Map<ICollection<ExpenseType>>(expenseTypeDtos);

            await _expenseTypeRepository.AddRangeAsync(expenseTypes);
            _memoryCache.Remove(AllExpenseTypeKey);
            return expenseTypeDtos;
        }

        public async Task<ICollection<ExpenseTypeDto>> GetAllAsync()
        {
            return await _memoryCache.GetOrCreateAsync(AllExpenseTypeKey, async flatsCache =>
            {
                flatsCache.SetOptions(_cacheOptions);
                var expenseTypes = await _expenseTypeRepository.GetAllAsync();

                return _mapper.Map<ICollection<ExpenseTypeDto>>(expenseTypes);
            });
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
            _memoryCache.Remove(AllExpenseTypeKey);
        }

        public async Task Update(UpdateExpenseTypeDto expenseTypeDto, int id)
        {
            var expenseType = await _expenseTypeRepository.GetByIdAsync(id);
            if (expenseType is null) throw new Exception("ExpenseType is not found");

            expenseType.ExpenseTypeName = expenseTypeDto.ExpenseTypeName;
            _expenseTypeRepository.Update(expenseType);
            _memoryCache.Remove(AllExpenseTypeKey);
            
        }
    }
}