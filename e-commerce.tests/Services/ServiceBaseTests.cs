using System;
using System.Threading.Tasks;
using AutoMapper;
using e_commerce.Server.DTOs;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories.Interfaces;
using e_commerce.Server.Services;
using e_commerce.Server.uniOfWork;
using Moq;
using Xunit;

namespace e_commerce.tests.Services
{
    public class ServiceBaseTests
    {
        private readonly Mock<IGenericRepository<Product>> _mockRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public ServiceBaseTests()
        {
            _mockRepository = new Mock<IGenericRepository<Product>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task AddAsync_Should_Add_Product()
        {
            // Arrange
            var dto = new ProductDTO(); // Create an instance of ProductDTO
            var entity = new Product(); // Create an instance of Product entity
            _mockMapper.Setup(mapper => mapper.Map<Product>(dto)).Returns(entity); // Setup mapper to return entity for ProductDTO

            var serviceBase = new TestProductServiceBase(_mockRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object); // Initialize TestServiceBase with Product and ProductDTO

            // Act
            await serviceBase.AddAsync(dto); // Call AddAsync with ProductDTO

            // Assert
            _mockRepository.Verify(repo => repo.AddAsync(entity), Times.Once()); // Verify repository AddAsync was called
            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.CompleteAsync(), Times.Once()); // Verify unit of work CompleteAsync was called
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Product()
        {
            // Arrange
            var dto = new ProductDTO(); // Create an instance of ProductDTO
            var entity = new Product(); // Create an instance of Product entity
            _mockMapper.Setup(mapper => mapper.Map<Product>(dto)).Returns(entity); // Setup mapper to return entity for ProductDTO

            var serviceBase = new TestProductServiceBase(_mockRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object); // Initialize TestServiceBase with Product and ProductDTO

            // Act
            await serviceBase.UpdateAsync(dto); // Call UpdateAsync with ProductDTO

            // Assert
            _mockRepository.Verify(repo => repo.UpdateAsync(entity), Times.Once()); // Verify repository UpdateAsync was called
            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.CompleteAsync(), Times.Once()); // Verify unit of work CompleteAsync was called
        }

        [Fact]
        public async Task DeleteAsync_Should_Delete_Product()
        {
            // Arrange
            var productId = 1; // Example product ID
            var entity = new Product { ProductId = productId }; // Create an instance of Product entity
            _mockRepository.Setup(repo => repo.DeleteAsync(productId)).Returns(Task.CompletedTask); // Setup repository to return completed task for DeleteAsync

            var serviceBase = new TestProductServiceBase(_mockRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object); // Initialize TestServiceBase with Product and ProductDTO

            // Act
            await serviceBase.DeleteAsync(productId); // Call DeleteAsync with product ID

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(productId), Times.Once()); // Verify repository DeleteAsync was called with correct product ID
            _mockUnitOfWork.Verify(unitOfWork => unitOfWork.CompleteAsync(), Times.Once()); // Verify unit of work CompleteAsync was called
        }

        #region TestServiceBase Implementation for Product

        private class TestProductServiceBase : ServiceBase<Product, ProductDTO>
        {
            public TestProductServiceBase(
                IGenericRepository<Product> repository,
                IUnitOfWork unitOfWork,
                IMapper mapper
            ) : base(repository, unitOfWork, mapper)
            {
            }

            protected override Task<Product> GetByIdAsync(int id)
            {
                // Simulate retrieving a Product entity by ID for testing
                var product = new Product { ProductId = id, Name = "Test Product" };
                return Task.FromResult(product);
            }
        }

        #endregion
    }
}
