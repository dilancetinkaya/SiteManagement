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
    public class BlockService : IBlockService
    {
        private readonly IBlockRepository _blockRepository;
        private readonly IMapper _mapper;

        public BlockService(IBlockRepository blockRepository, IMapper mapper)
        {
            _blockRepository = blockRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateBlockDto blockDto)
        {
            var block = _mapper.Map<Block>(blockDto);
            await _blockRepository.AddAsync(block);
        }

        public async Task<ICollection<CreateBlockDto>> AddRangeAsync(ICollection<CreateBlockDto> blockDtos)
        {
            var blocks = _mapper.Map<ICollection<Block>>(blockDtos);
            await _blockRepository.AddRangeAsync(blocks);
            return blockDtos;

        }

        public async Task<ICollection<BlockDto>> GetAllAsync()
        {
            var blocks = await _blockRepository.GetAllAsync();
            return _mapper.Map<ICollection<BlockDto>>(blocks);
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
        }

        public UpdateBlockDto Update(UpdateBlockDto blockDto, int id)
        {
            var updatedBlock = _mapper.Map<Block>(blockDto);
            updatedBlock.Id = id;
            _blockRepository.Update(updatedBlock);
            return blockDto;
        }
    }
}
