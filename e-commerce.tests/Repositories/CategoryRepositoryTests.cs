using e_commerce.Server.Data;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace e_commerce.tests.Repositories
{
    /// <summary>
    /// Unit tests for the CategoryRepository class.
    /// </summary>
    public class CategoryRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly CategoryRepository _categoryRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepositoryTests"/> class.
        /// Sets up the in-memory database and seeds it with initial data.
        /// </summary>
        public CategoryRepositoryTests()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _categoryRepository = new CategoryRepository(_context);

            SeedDatabase();
        }

        /// <summary>
        /// Seeds the in-memory database with initial data.
        /// </summary>
        private void SeedDatabase()
        {
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Electronics" },
                new Category { CategoryId = 2, Name = "Books" }
            };

            _context.Categories.AddRange(categories);
            _context.SaveChanges();
        }

        /// <summary>
        /// Tests that GetAllCategoriesAsync method returns all categories.
        /// </summary>
        [Fact]
        public async Task GetAllCategoriesAsync_Should_Return_All_Categories()
        {
            // Act
            var result = await _categoryRepository.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }


        /// <summary>
        /// Tests that GetCategoryByIdAsync method returns the correct category for a valid ID.
        /// </summary>
        [Fact]
        public async Task GetCategoryByIdAsync_Should_Return_Correct_Category()
        {
            // Act
            var result = await _categoryRepository.GetCategoryByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CategoryId);
            Assert.Equal("Electronics", result.Name);
        }

        /// <summary>
        /// Tests that GetCategoryByIdAsync method returns null for an invalid ID.
        /// </summary>
        [Fact]
        public async Task GetCategoryByIdAsync_Should_Return_Null_For_Invalid_Id()
        {
            // Act
            var result = await _categoryRepository.GetCategoryByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

    }
}
