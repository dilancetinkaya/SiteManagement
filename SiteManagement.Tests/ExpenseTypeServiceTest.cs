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
    public class ExpenseTypeServiceTest
    {
        private readonly Mock<IExpenseTypeRepository> _expenseTypeRepositoryMock;

        private readonly Mock<IMapper> _mapperMock;

        private readonly Mock<IMemoryCache> _memoryCacheMock;

        private readonly ExpenseTypeService _expenseTypeService;


        public ExpenseTypeServiceTest()
        {
            _expenseTypeRepositoryMock = new Mock<IExpenseTypeRepository>();

            _mapperMock = new Mock<IMapper>();

            _memoryCacheMock = new Mock<IMemoryCache>();

            _expenseTypeService = new ExpenseTypeService(_expenseTypeRepositoryMock.Object, _mapperMock.Object, _memoryCacheMock.Object);

        }

        [Fact]
        public async Task GetById_When_ExpenseType_NotExists_Throw_Exception()
        {
            //Arrange
            _expenseTypeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Throws(new Exception("ExpenseType is not found"));

            //Act assert
            await Assert.ThrowsAsync<Exception>(() => _expenseTypeService.GetByIdAsync(1));
        }

        [Fact]
        public async Task GetById_Return_ExpenseType()
        {
            //Arrange
            var ExpenseType = new ExpenseTypeDto { Id = 1, ExpenseTypeName = "ExpenseType"};

            _expenseTypeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Domain.Entities.ExpenseType { Id = 1, ExpenseTypeName = "ExpenseType" });

            _mapperMock.Setup(x => x.Map<ExpenseTypeDto>(It.IsAny<ExpenseType>()))
                       .Returns((ExpenseType source) =>
                       {
                           return ExpenseType;
                       });

            //Act 
            var result = await _expenseTypeService.GetByIdAsync(1);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(ExpenseType.ExpenseTypeName, result.ExpenseTypeName);
        }

        [Fact]
        public async Task Remove_When_ExpenseType_NotExists_Throw_Exception()
        {
            //Arrange
            _expenseTypeRepositoryMock.Setup(x => x.Remove(It.IsAny<ExpenseType>())).Throws(new Exception("ExpenseType is not found"));

            //Act assert
            await Assert.ThrowsAsync<Exception>(() => _expenseTypeService.RemoveAsync(1));
        }

        [Fact]
        public async Task Remove_Success()
        {
            //Arrange
            _expenseTypeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Domain.Entities.ExpenseType { Id = 1, ExpenseTypeName = "ExpenseType" });

            _expenseTypeRepositoryMock.Setup(x => x.Remove(It.IsAny<ExpenseType>()));

            _memoryCacheMock.Setup(x => x.Remove(It.IsAny<Object>()));

            //Act
            await _expenseTypeService.RemoveAsync(1);

            //Assert
            _expenseTypeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);

            _expenseTypeRepositoryMock.Verify(x => x.Remove(It.IsAny<ExpenseType>()), Times.Once);

            _memoryCacheMock.Verify(x => x.Remove(It.IsAny<Object>()), Times.Once);

        }

        [Fact]
        public async Task Update_When_ExpenseType_NotExists_Throw_Exception()
        {
            //Arrange
            _expenseTypeRepositoryMock.Setup(x => x.Update(It.IsAny<ExpenseType>())).Throws(new Exception("ExpenseType is not found"));

            //Act assert
            await Assert.ThrowsAsync<Exception>(() => _expenseTypeService.Update(null, 1));
        }

        [Fact]
        public async Task Update_Success()
        {
            //Arrange
            var ExpenseType = new UpdateExpenseTypeDto { ExpenseTypeName = "ExpenseType"};

            _expenseTypeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Domain.Entities.ExpenseType { Id = 1, ExpenseTypeName = "ExpenseType" });

            _expenseTypeRepositoryMock.Setup(x => x.Update(It.IsAny<ExpenseType>()));

            _memoryCacheMock.Setup(x => x.Remove(It.IsAny<Object>()));

            //Act
            await _expenseTypeService.Update(ExpenseType, 1);

            //Assert
            _expenseTypeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);

            _expenseTypeRepositoryMock.Verify(x => x.Update(It.IsAny<ExpenseType>()), Times.Once);

            _memoryCacheMock.Verify(x => x.Remove(It.IsAny<Object>()), Times.Once);

        }
    }
}
