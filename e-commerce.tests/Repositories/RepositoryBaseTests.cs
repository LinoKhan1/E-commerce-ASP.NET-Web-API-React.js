using e_commerce.Server.Data;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace e_commerce.tests.Repositories
{
    /// <summary>
    /// Unit tests for the RepositoryBase class.
    /// </summary>
    public class RepositoryBaseTests
    {
        private readonly ApplicationDbContext _context;
        private readonly TestRepository _testRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBaseTests"/> class.
        /// Sets up the in-memory database and the test repository.
        /// </summary>
        public RepositoryBaseTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _testRepository = new TestRepository(_context);

            SeedDatabase();

        }

        /// <summary>
        /// Seeds the in-memory database with initial data.
        /// </summary>
        private void SeedDatabase()
        {
            var categories = new[]
            {
                new Category { CategoryId = 1, Name = "Electronics" },
                new Category { CategoryId = 2, Name = "Books" }
            };

            _context.Categories.AddRange(categories);
            _context.SaveChanges();
        }

        /// <summary>
        /// Tests that AddAsync method adds a new entity to the database.
        /// </summary>
        [Fact]
        public async Task AddAsync_Should_Add_New_Entity()
        {
            // Arrange
            var newCategory = new Category { CategoryId = 3, Name = "Clothing" };

            // Act
            await _testRepository.AddAsync(newCategory);

            // Assert
            var category = await _context.Categories.FindAsync(3);
            Assert.NotNull(category);
            Assert.Equal("Clothing", category.Name);
        }


        /// <summary>
        /// Tests that UpdateAsync method updates an existing entity in the database.
        /// </summary>
        [Fact]
        public async Task UpdateAsync_Should_Update_Existing_Entity()
        {

            // Arrange
            var category = await _context.Categories.FindAsync(1);
            Assert.NotNull(category);
            category.Name = "Updated Electronics";

            // act
            await _testRepository.UpdateAsync(category);

            // Assert
            var updatedCategory = await _context.Categories.FindAsync(1);
            Assert.NotNull(updatedCategory);
            Assert.Equal("Updated Electronics", updatedCategory.Name);
        }
        /// <summary>
        /// Tests that DeleteAsync method deletes an entity from the database.
        /// </summary>
        [Fact]
        public async Task DeleteAsync_Should_Delete_Entity()
        {
            // Act
            await _testRepository.DeleteAsync(1);

            // Assert
            var deletedCategory = await _context.Categories.FindAsync(1);
            Assert.Null(deletedCategory);
        }

        /// <summary>
        /// Tests that AddAsync method throws an ArgumentNullException when the entity is null.
        /// </summary>
        [Fact]
        public async Task AddAsync_Should_Throw_ArgumentNullException_When_Entity_Is_Null()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _testRepository.AddAsync(null));
        }

        /// <summary>
        /// Tests that UpdateAsync method throws an ArgumentNullException when the entity is null.
        /// </summary>
        [Fact]
        public async Task UpdateAsync_Should_Throw_ArgumentNullException_When_Entity_Is_Null()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _testRepository.UpdateAsync(null));
        }


    }

    /// <summary>
    /// A test repository that derives from RepositoryBase for testing purposes.
    /// </summary>
    public class TestRepository : RepositoryBase<Category>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public TestRepository(ApplicationDbContext context) : base(context)
        {


        }

    }

}
