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
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private const string AllBuildingKey = "BUILDINGALL";
        private MemoryCacheEntryOptions _cacheOptions;

        public BuildingService(IBuildingRepository buildingRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative: TimeSpan.FromMinutes(10));
            _buildingRepository = buildingRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateBuildingDto buildingDto)
        {
            var building = _mapper.Map<Building>(buildingDto);

            await _buildingRepository.AddAsync(building);

            _memoryCache.Remove(AllBuildingKey);
        }

        public async Task<ICollection<CreateBuildingDto>> AddRangeAsync(ICollection<CreateBuildingDto> buildingDtos)
        {
            var buildings = _mapper.Map<ICollection<Building>>(buildingDtos);

            await _buildingRepository.AddRangeAsync(buildings);

            _memoryCache.Remove(AllBuildingKey);

            return buildingDtos;
        }

        public async Task<ICollection<BuildingDto>> GetAllAsync()
        {
            return await _memoryCache.GetOrCreateAsync(AllBuildingKey, async flatsCache =>
            {
                flatsCache.SetOptions(_cacheOptions);

                var buildings = await _buildingRepository.GetAllAsync();
                return _mapper.Map<ICollection<BuildingDto>>(buildings);
            });
        }

        public async Task<BuildingDto> GetByIdAsync(int id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);

            if (building is null) throw new Exception("Building is not found");

            var buildingDto = _mapper.Map<BuildingDto>(building);

            return buildingDto;
        }

        public async Task RemoveAsync(int id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);

            if (building is null) throw new Exception("Building is not found");

            _buildingRepository.Remove(building);

            _memoryCache.Remove(AllBuildingKey);
        }

        public async Task UpdateAsync(UpdateBuildingDto buildingDto, int id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);

            if (building is null) throw new Exception("Building is not found");

            building.BuildingName = buildingDto.BuildingName;
            building.TotalFlat = buildingDto.TotalFlat;
            building.BlockId = buildingDto.BlockId;
            _buildingRepository.Update(building);
            _memoryCache.Remove(AllBuildingKey);
        }
    }
}
