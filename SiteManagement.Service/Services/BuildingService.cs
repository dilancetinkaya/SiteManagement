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
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IMapper _mapper;

        public BuildingService(IBuildingRepository buildingRepository, IMapper mapper)
        {
            _buildingRepository = buildingRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateBuildingDto buildingDto)
        {
            var building = _mapper.Map<Building>(buildingDto);
            await _buildingRepository.AddAsync(building);
        }

        public async Task<ICollection<CreateBuildingDto>> AddRangeAsync(ICollection<CreateBuildingDto> buildingDtos)
        {
            var buildings = _mapper.Map<ICollection<Building>>(buildingDtos);
            await _buildingRepository.AddRangeAsync(buildings);
            return buildingDtos;
        }

        public async Task<ICollection<BuildingDto>> GetAllAsync()
        {
            var buildings = await _buildingRepository.GetAllAsync();
            return _mapper.Map<ICollection<BuildingDto>>(buildings);
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
        }

        public UpdateBuildingDto Update(UpdateBuildingDto buildingDto, int id)
        {
            var updatedBuilding = _mapper.Map<Building>(buildingDto);
            updatedBuilding.Id = id;
            _buildingRepository.Update(updatedBuilding);
            return buildingDto;
        }
    }
}
