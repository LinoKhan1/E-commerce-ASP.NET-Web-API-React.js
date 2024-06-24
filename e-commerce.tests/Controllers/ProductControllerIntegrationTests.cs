using e_commerce.Server.Controllers;
using e_commerce.Server.DTOs;
using e_commerce.Server.Models;
using e_commerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace e_commerce.Tests
{
    /// <summary>
    /// Unit tests for the ProductsController.
    /// </summary>
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductsController _controller;

        /// <summary>
        /// Initializes a new instance of the ProductsControllerTests class.
        /// </summary>
        public ProductsControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductsController(_mockProductService.Object);
        }

        /// <summary>
        /// Tests the GetProducts method of ProductsController.
        /// </summary>
        [Fact]
        public async Task GetProducts_ReturnsOkResult()
        {
            // Arrange
            var mockProducts = new List<ProductDTO>
            {
                new ProductDTO { ProductId = 1, Name = "Product 1" },
                new ProductDTO { ProductId = 2, Name = "Product 2" }
            };
            _mockProductService.Setup(service => service.GetAllProductsAsync())
                               .ReturnsAsync(mockProducts);

            // Act
            var result = await _controller.GetProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(okResult.Value);
            Assert.Equal(2, products.Count());
        }

        /// <summary>
        /// Tests the GetProduct method of ProductsController.
        /// </summary>
        [Fact]
        public async Task GetProduct_ReturnsOkResult()
        {
            // Arrange
            int productId = 1;
            var mockProduct = new ProductDTO { ProductId = productId, Name = "Product 1" };
            _mockProductService.Setup(service => service.GetProductByIdAsync(productId))
                               .ReturnsAsync(mockProduct);

            // Act
            var result = await _controller.GetProduct(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var product = Assert.IsType<ProductDTO>(okResult.Value);
            Assert.Equal(productId, product.ProductId);
        }


        /// <summary>
        /// Tests the PostProduct method of ProductsController.
        /// </summary>
        [Fact]
        public async Task PostProduct_ReturnsCreatedAtAction()
        {
            // Arrange
            var newProduct = new ProductDTO { ProductId = 3, Name = "New Product" };

            // Act
            var result = await _controller.PostProduct(newProduct);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(_controller.GetProduct), createdAtActionResult.ActionName);
            Assert.Equal(newProduct.ProductId, createdAtActionResult.RouteValues["id"]);
        }

        /// <summary>
        /// Tests the PutProduct method of ProductsController.
        /// </summary>
        [Fact]
        public async Task PutProduct_ReturnsNoContentResult()
        {
            // Arrange
            int productId = 1;
            var productToUpdate = new ProductDTO { ProductId = productId, Name = "Updated Product" };

            // Act
            var result = await _controller.PutProduct(productId, productToUpdate);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        /// <summary>
        /// Tests the DeleteProduct method of ProductsController.
        /// </summary>
        [Fact]
        public async Task DeleteProduct_ReturnsNoContentResult()
        {
            // Arrange
            int productId = 1;

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
