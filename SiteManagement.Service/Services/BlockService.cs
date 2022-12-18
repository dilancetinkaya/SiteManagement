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
    public class BlockService : IBlockService
    {
        private readonly IBlockRepository _blockRepository;
        private readonly IMapper _mapper;

        private readonly IMemoryCache _memoryCache;
        private const string AllBlockKey = "BLOCKALL";
        private MemoryCacheEntryOptions _cacheOptions;

        public BlockService(IBlockRepository blockRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative: TimeSpan.FromMinutes(10));
            _blockRepository = blockRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateBlockDto blockDto)
        {
            var block = _mapper.Map<Block>(blockDto);

            await _blockRepository.AddAsync(block);
            _memoryCache.Remove(AllBlockKey);
        }

        public async Task<ICollection<CreateBlockDto>> AddRangeAsync(ICollection<CreateBlockDto> blockDtos)
        {
            var blocks = _mapper.Map<ICollection<Block>>(blockDtos);

            await _blockRepository.AddRangeAsync(blocks);
            _memoryCache.Remove(AllBlockKey);
            return blockDtos;
        }

        public async Task<ICollection<BlockDto>> GetAllAsync()
        {
            return await _memoryCache.GetOrCreateAsync(AllBlockKey, async flatsCache =>
            {
                flatsCache.SetOptions(_cacheOptions);
                var blocks = await _blockRepository.GetAllAsync();
                return _mapper.Map<ICollection<BlockDto>>(blocks);
            });
        }

        public async Task<BlockDto> GetByIdAsync(int id)
        {
            var block = await _blockRepository.GetByIdAsync(id);

            if (block is null) throw new Exception("Block is not found");
            return _mapper.Map<BlockDto>(block);
        }

        public async Task RemoveAsync(int id)
        {
            var block = await _blockRepository.GetByIdAsync(id);

            if (block is null) throw new Exception("Block is not found");

            _blockRepository.Remove(block);
            _memoryCache.Remove(AllBlockKey);
        }

        public async Task UpdateAsync(UpdateBlockDto blockDto, int id)
        {
            var block = await _blockRepository.GetByIdAsync(id);

            if (block is null) throw new Exception("Block is not found");

            block.BlockName = blockDto.BlockName;
            _blockRepository.Update(block);
            _memoryCache.Remove(AllBlockKey);
        }
    }
}