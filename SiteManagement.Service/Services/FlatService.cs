using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
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
    public class FlatService : IFlatService
    {
        private readonly IFlatRepository _flatRepository;
        private readonly IBuildingService _buildingService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private const string FlatsByRelationsKey = "FLATSRELATIONS";
        private const string AllFlatsKey = "FLATSALL";
        private MemoryCacheEntryOptions _cacheOptions;

        public FlatService(IFlatRepository flatRepository, IMapper mapper, IBuildingService buildingService,
                          IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative: TimeSpan.FromMinutes(5));
            _flatRepository = flatRepository;
            _buildingService = buildingService;
            _mapper = mapper;

        }

        public async Task AddAsync(CreateFlatDto flatDto)
        {
            var building = await _buildingService.GetByIdAsync(flatDto.BuildingId);

            var totalFlat = (await _flatRepository.GetWhereAsync(x => x.BuildingId == flatDto.BuildingId)).Count();
            if (building.TotalFlat < totalFlat) throw new Exception("Building is full, no more flats can be added");

            var flat = _mapper.Map<Flat>(flatDto);
            await _flatRepository.AddAsync(flat);
            _memoryCache.Remove(FlatsByRelationsKey);
            _memoryCache.Remove(AllFlatsKey);
        }

        public async Task<ICollection<CreateFlatDto>> AddRangeAsync(ICollection<CreateFlatDto> flatDtos)
        {
            var flats = _mapper.Map<ICollection<Flat>>(flatDtos);
            await _flatRepository.AddRangeAsync(flats);
            _memoryCache.Remove(FlatsByRelationsKey);
            _memoryCache.Remove(AllFlatsKey);
            return flatDtos;
        }

        public async Task<ICollection<FlatDto>> GetAllAsync()
        {

            return await _memoryCache.GetOrCreateAsync(AllFlatsKey, async flatsCache =>
            {
                flatsCache.SetOptions(_cacheOptions);
                var flats = await _flatRepository.GetAllAsync();
                return _mapper.Map<ICollection<FlatDto>>(flats);
            });
        }

        public async Task<FlatDto> GetByIdAsync(int id)
        {
            var flat = await _flatRepository.GetByIdAsync(id);

            if (flat is null) throw new Exception("Flat is not found");

            var flatDto = _mapper.Map<FlatDto>(flat);
            return flatDto;
        }

        /// <summary>
        /// Flatleri iliskili oldugu verilerle getirme
        /// </summary>
        public async Task<ICollection<FlatDto>> GetAllFlatsByRelations()
        {

            return await _memoryCache.GetOrCreateAsync(FlatsByRelationsKey, async flatsCache =>
            {
                flatsCache.SetOptions(_cacheOptions);
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
            });
        }

        public async Task RemoveAsync(int id)
        {
            var flat = await _flatRepository.GetByIdAsync(id);

            if (flat is null) throw new Exception("Flat is not found");

            _flatRepository.Remove(flat);
            _memoryCache.Remove(FlatsByRelationsKey);
            _memoryCache.Remove(AllFlatsKey);
        }

        public async Task UpdateAsync(UpdateFlatDto flatDto, int id)
        {
            var flat = await _flatRepository.GetByIdAsync(id);
            if (flat is null) throw new Exception("Flat is not found");

            flat.FloorNumber = flatDto.FlatNumber;
            flat.IsOwner = flatDto.IsOwner;
            flat.IsEmpty = flatDto.IsEmpty;
            _flatRepository.Update(flat);
            _memoryCache.Remove(FlatsByRelationsKey);
            _memoryCache.Remove(AllFlatsKey);
        }

        /// <summary>
        /// Flatte bulunan kullanıcıyı günceller
        /// </summary>
        public async Task UpdateFlatUserAsync(UpdateFlatUserDto flatDto, int id)
        {
            var flat = await _flatRepository.GetByIdAsync(id);
            if (flat is null) throw new Exception("Flat is not found");

            flat.UserId = flatDto.UserId;
            flat.IsOwner = flatDto.IsOwner;
            _flatRepository.Update(flat);
            _memoryCache.Remove(FlatsByRelationsKey);
            _memoryCache.Remove(AllFlatsKey);
        }
    }
}

