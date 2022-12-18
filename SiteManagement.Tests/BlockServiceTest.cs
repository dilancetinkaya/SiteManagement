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
    public class BlockServiceTest
    {
        private readonly Mock<IBlockRepository> _blockRepositoryMock;

        private readonly Mock<IMapper> _mapperMock;

        private readonly Mock<IMemoryCache> _memoryCacheMock;

        private readonly BlockService _blockService;


        public BlockServiceTest()
        {
            _blockRepositoryMock = new Mock<IBlockRepository>();

            _mapperMock = new Mock<IMapper>();

            _memoryCacheMock = new Mock<IMemoryCache>();

            _blockService = new BlockService(_blockRepositoryMock.Object, _mapperMock.Object, _memoryCacheMock.Object);

        }

        [Fact]
        public async Task GetById_When_Block_NotExists_Throw_Exception()
        {
            //Arrange
            _blockRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Throws(new Exception("Block is not found"));

            //Act assert
            await Assert.ThrowsAsync<Exception>(() => _blockService.GetByIdAsync(1));
        }

        [Fact]
        public async Task GetById_Return_Block()
        {
            //Arrange
            var block = new BlockDto { Id = 1, BlockName = "Block" };

            _blockRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Domain.Entities.Block { Id = 1, BlockName = "Block" });

            _mapperMock.Setup(x => x.Map<BlockDto>(It.IsAny<Block>()))
                       .Returns((Block source) =>
                       {
                           return block;
                       });

            //Act 
            var result = await _blockService.GetByIdAsync(1);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(block.BlockName, result.BlockName);
        }

        [Fact]
        public async Task Remove_When_Block_NotExists_Throw_Exception()
        {
            //Arrange
            _blockRepositoryMock.Setup(x => x.Remove(It.IsAny<Block>())).Throws(new Exception("Block is not found"));

            //Act assert
            await Assert.ThrowsAsync<Exception>(() => _blockService.RemoveAsync(1));
        }

        [Fact]
        public async Task Remove_Success()
        {
            //Arrange
            _blockRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Domain.Entities.Block { Id = 1, BlockName = "Block" });

            _blockRepositoryMock.Setup(x => x.Remove(It.IsAny<Block>()));

            _memoryCacheMock.Setup(x => x.Remove(It.IsAny<Object>()));

            //Act
            await _blockService.RemoveAsync(1);

            //Assert
            _blockRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);

            _blockRepositoryMock.Verify(x => x.Remove(It.IsAny<Block>()), Times.Once);

            _memoryCacheMock.Verify(x => x.Remove(It.IsAny<Object>()), Times.Once);

        }

        [Fact]
        public async Task Update_When_Block_NotExists_Throw_Exception()
        {
            //Arrange
            _blockRepositoryMock.Setup(x => x.Update(It.IsAny<Block>())).Throws(new Exception("Block is not found"));

            //Act assert
            await Assert.ThrowsAsync<Exception>(() => _blockService.UpdateAsync(null, 1));
        }

        [Fact]
        public async Task Update_Success()
        {
            //Arrange
            var block = new UpdateBlockDto { BlockName = "Block" };

            _blockRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Domain.Entities.Block { Id = 1, BlockName = "Block" });

            _blockRepositoryMock.Setup(x => x.Update(It.IsAny<Block>()));

            _memoryCacheMock.Setup(x => x.Remove(It.IsAny<Object>()));

            //Act
            await _blockService.UpdateAsync(block, 1);

            //Assert
            _blockRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);

            _blockRepositoryMock.Verify(x => x.Update(It.IsAny<Block>()), Times.Once);

            _memoryCacheMock.Verify(x => x.Remove(It.IsAny<Object>()), Times.Once);

        }
    }
}
