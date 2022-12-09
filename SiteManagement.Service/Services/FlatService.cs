using AutoMapper;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using SiteManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Service.Services
{
    public class FlatService : IFlatService
    {
        private readonly IFlatRepository _flatRepository;
        private readonly IMapper _mapper;

        public FlatService(IFlatRepository flatRepository, IMapper mapper)
        {
            _flatRepository = flatRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateFlatDto flatDto)
        {
            var flat = _mapper.Map<Flat>(flatDto);
            await _flatRepository.AddAsync(flat);
        }

        public async Task<ICollection<CreateFlatDto>> AddRangeAsync(ICollection<CreateFlatDto> flatDtos)
        {
            var flats = _mapper.Map<ICollection<Flat>>(flatDtos);
            await _flatRepository.AddRangeAsync(flats);
            return flatDtos;
        }

        public async Task<ICollection<FlatDto>> GetAllAsync()
        {
            var flats = await _flatRepository.GetAllAsync();
            return _mapper.Map<ICollection<FlatDto>>(flats);
        }

        public async Task<FlatDto> GetByIdAsync(int id)
        {
            var flat = await _flatRepository.GetByIdAsync(id);
            if (flat is null) throw new Exception("Flat is not found");

            var flatDto = _mapper.Map<FlatDto>(flat);
            return flatDto;
        }

        public async Task RemoveAsync(int id)
        {
            var flat = await _flatRepository.GetByIdAsync(id);
            if (flat is null) throw new Exception("Flat is not found");

            _flatRepository.Remove(flat);
        }

        public UpdateFlatDto Update(UpdateFlatDto flatDto, int id)
        {
            var updatedFlat = _mapper.Map<Flat>(flatDto);
            updatedFlat.Id = id;
            _flatRepository.Update(updatedFlat);
            return flatDto;
        }
        public async Task<ICollection<FlatDto>> GetAllFlatsByRelations()
        {
            var flats = await _flatRepository.GetAllFlatsByRelations();
            var flatDtos = flats.Select(f => new FlatDto()
            {
                Id = f.Id,
                FlatNumber = f.FlatNumber,
                TypeOfFlat = f.TypeOfFlat,
                IsEmpty = f.IsEmpty,
                BuildingId = f.BuildingId,
                UserId = f.UserId,
                FloorNumber = f.FloorNumber,
                IsOwner = f.IsOwner,
            }).ToList();
            return flatDtos;
        }
    }
}

