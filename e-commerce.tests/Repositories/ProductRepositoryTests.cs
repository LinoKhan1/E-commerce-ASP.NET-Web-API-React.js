using e_commerce.Server.Data;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace e_commerce.tests.Repositories
{
    /// <summary>
    /// Unit tests for the ProductRepository class.
    /// </summary>
    public class ProductRepositoryTests
    {

        private readonly ApplicationDbContext _context;
        private readonly ProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepositoryTests"/> class.
        /// Sets up the in-memory database and seeds it with initial data.
        /// </summary>
        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _productRepository = new ProductRepository(_context);


            SeedDatabase();
        }

        /// <summary>
        /// Seeds the in-memory database with initial data.
        /// </summary>
        private void SeedDatabase()
        {
            var categories = new List<Category>
            {
                new Category {CategoryId = 1, Name = "Electronic"},
                new Category {CategoryId = 2, Name = "Books" }

            };

            var products = new List<Product>
            {
                new Product {ProductId = 1, Name ="Laptop", Description="Test Description", Price=35099, Stock=50, CategoryId=1, Category = categories[0]},
                new Product {ProductId = 2, Name = "Smartphone", Description="Test Description", Price=35099, Stock=50, CategoryId= 1, Category = categories[0]},
                new Product {ProductId = 3, Name = "Novel", Description="Test Description", Price=349, Stock=50, CategoryId=2, Category = categories[1]}
            };

            _context.Categories.AddRange(categories);
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }

        /// <summary>
        /// Tests that GetAllProductsAsync method returns all products.
        /// </summary>
        [Fact]
        public async Task GetAllProductsAsync_Should_Return_All_Products()
        {
            // Act
            var result = await _productRepository.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        /// <summary>
        /// Tests that GetProductByIdAsync method returns the correct product for a valid ID.
        /// </summary>
        [Fact]
        public async Task GetProductByIdAsync_Should_Return_Correct_Product()
        {
            // Act
            var result = await _productRepository.GetProductByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ProductId);
            Assert.Equal("Laptop", result.Name);
        }


        /// <summary>
        /// Tests that GetProductByIdAsync method returns null for an invalid ID.
        /// </summary>
        [Fact]
        public async Task GetProductByIdAsync_Should_Return_Null_For_Invalid_Id()
        {
            // Act
            var result = await _productRepository.GetProductByIdAsync(99);

            // Assert
            Assert.Null(result);
        }
    }
}

