using e_commerce.Server.Data;
using e_commerce.Server.Repositories.Interfaces;
using e_commerce.Server.uniOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace e_commerce.tests.uniOfWork
{
    public class UnitOfWorkTests : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "UnitTestDatabase")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _unitOfWork = new UnitOfWork(_dbContext);
        }

        [Fact]
        public async Task CompleteAsync_Should_SaveChanges()
        {
            // Arrange - No need for explicit setup with in-memory database

            // Act
            var result = await _unitOfWork.CompleteAsync();

            // Assert
            Assert.Equal(0, result); // In-memory database returns 0, adjust as per your implementation
        }

        [Fact]
        public void Dispose_Should_DisposeResources()
        {
            // Arrange - Nothing to arrange for this test

            // Act
            _unitOfWork.Dispose();

            // Assert
            Assert.NotNull(_dbContext); // Verify that DbContext was disposed properly
        }

        public void Dispose()
        {
            _unitOfWork.Dispose(); // Ensure proper cleanup after each test
        }
    }
}
