using AutoMapper;
using e_commerce.Server.Data;
using e_commerce.Server.DTOs;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories;
using e_commerce.tests.Repositories;
using e_commerce.Server.uniOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using e_commerce.Server.Repositories.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using e_commerce.Server.Services;

namespace e_commerce.tests.Services
{
    // <summary>
    /// Unit tests for the ProductService class.
    /// </summary>
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;

        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductRepository>();   
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            // AutoMapper configurations for test
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile()); // Register the AutoMapperProfile
            });

            _mapper = mapperConfig.CreateMapper();
        }


        /// <summary>
        /// Tests GetAllProductsAsync method of ProductService.
        /// </summary>
        [Fact]
        public async Task GetAllProductAsync_Should_Return_All_Products()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product 1", Description = "Test Description 1", Price = 10.0m, Stock = 50 },
                new Product { ProductId = 2, Name = "Product 2", Description = "Test Description 2", Price = 15.0m, Stock = 50 }
            };

            _repositoryMock.Setup(repo => repo.GetAllProductsAsync()).ReturnsAsync(products);

            var productService = new ProductService(_repositoryMock.Object, _unitOfWorkMock.Object, _mapper);

            // Act
            var result = await productService.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            // Additional assertions to verify properties of ProductDTO
            Assert.Collection(result,
                item =>
                {
                    Assert.Equal(1, item.ProductId);
                    Assert.Equal("Product 1", item.Name);
                    Assert.Equal(10.0m, item.Price);
                    // Add more assertions for other properties if needed
                },
                item =>
                {
                    Assert.Equal(2, item.ProductId);
                    Assert.Equal("Product 2", item.Name);
                    Assert.Equal(15.0m, item.Price);
                    // Add more assertions for other properties if needed
                });


        }

        /// <summary>
        /// Tests GetProductByIdAsync method of ProductService.
        /// </summary>
        [Fact]
        public async Task GetProductByIdAsync_Should_Return_Correct_Product()
        {
            // Arrange
            int productId = 1;
            var product = new Product { ProductId = productId, Name = "Product 1", Price = 10.0m };
            _repositoryMock.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync(product);

            var productService = new ProductService(_repositoryMock.Object, _unitOfWorkMock.Object, _mapper);

            // Act
            var result = await productService.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.ProductId);
            Assert.Equal("Product 1", result.Name);
            Assert.Equal(10.0m, result.Price);

        }

    }
}
