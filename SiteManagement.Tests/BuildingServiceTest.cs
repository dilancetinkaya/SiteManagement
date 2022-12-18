using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Service.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SiteManagement.Tests
{
    public class BuildingServiceTest
    {
        private readonly Mock<IBuildingRepository> _buildingRepositoryMock;

        private readonly Mock<IMapper> _mapperMock;

        private readonly Mock<IMemoryCache> _memoryCacheMock;

        private readonly BuildingService _buildingService;


        public BuildingServiceTest()
        {
            _buildingRepositoryMock = new Mock<IBuildingRepository>();

            _mapperMock = new Mock<IMapper>();

            _memoryCacheMock = new Mock<IMemoryCache>();

            _buildingService = new BuildingService(_buildingRepositoryMock.Object, _mapperMock.Object, _memoryCacheMock.Object);

        }

        [Fact]
        public async Task GetById_When_Building_NotExists_Throw_Exception()
        {
            //Arrange
            _buildingRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Throws(new Exception("Building is not found"));

            //Act assert
            await Assert.ThrowsAsync<Exception>(() => _buildingService.GetByIdAsync(1));
        }

        [Fact]
        public async Task GetById_Return_Building()
        {
            //Arrange
            var Building = new BuildingDto { Id = 1, BuildingName = "Building", BlockId=3, TotalFlat=5 };

            _buildingRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Domain.Entities.Building { Id = 1, BuildingName = "Building" });

            _mapperMock.Setup(x => x.Map<BuildingDto>(It.IsAny<Building>()))
                       .Returns((Building source) =>
                       {
                           return Building;
                       });

            //Act 
            var result = await _buildingService.GetByIdAsync(1);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(Building.BuildingName, result.BuildingName);
        }

        [Fact]
        public async Task Remove_When_Building_NotExists_Throw_Exception()
        {
            //Arrange
            _buildingRepositoryMock.Setup(x => x.Remove(It.IsAny<Building>())).Throws(new Exception("Building is not found"));

            //Act assert
            await Assert.ThrowsAsync<Exception>(() => _buildingService.RemoveAsync(1));
        }

        [Fact]
        public async Task Remove_Success()
        {
            //Arrange
            _buildingRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Domain.Entities.Building { Id = 1, BuildingName = "Building" });

            _buildingRepositoryMock.Setup(x => x.Remove(It.IsAny<Building>()));

            _memoryCacheMock.Setup(x => x.Remove(It.IsAny<Object>()));

            //Act
            await _buildingService.RemoveAsync(1);

            //Assert
            _buildingRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);

            _buildingRepositoryMock.Verify(x => x.Remove(It.IsAny<Building>()), Times.Once);

            _memoryCacheMock.Verify(x => x.Remove(It.IsAny<Object>()), Times.Once);

        }

        [Fact]
        public async Task Update_When_Building_NotExists_Throw_Exception()
        {
            //Arrange
            _buildingRepositoryMock.Setup(x => x.Update(It.IsAny<Building>())).Throws(new Exception("Building is not found"));

            //Act assert
            await Assert.ThrowsAsync<Exception>(() => _buildingService.UpdateAsync(null, 1));
        }

        [Fact]
        public async Task Update_Success()
        {
            //Arrange
            var Building = new UpdateBuildingDto { BuildingName = "Building", TotalFlat=44, BlockId=5 };

            _buildingRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Domain.Entities.Building { Id = 1, BuildingName = "Building" });

            _buildingRepositoryMock.Setup(x => x.Update(It.IsAny<Building>()));

            _memoryCacheMock.Setup(x => x.Remove(It.IsAny<Object>()));

            //Act
            await _buildingService.UpdateAsync(Building, 1);

            //Assert
            _buildingRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);

            _buildingRepositoryMock.Verify(x => x.Update(It.IsAny<Building>()), Times.Once);

            _memoryCacheMock.Verify(x => x.Remove(It.IsAny<Object>()), Times.Once);

        }
    }
}
