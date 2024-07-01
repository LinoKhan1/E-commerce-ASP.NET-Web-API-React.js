using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using e_commerce.Server.Controllers;
using e_commerce.Server.DTOs;
using e_commerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace e_commerce.tests.Controllers
{
    public class CartControllerTests
    {
        private readonly Mock<ICartService> _mockCartService;
        private readonly CartController _controller;

        public CartControllerTests()
        {
            _mockCartService = new Mock<ICartService>();
            _controller = new CartController(_mockCartService.Object);
        }

        [Fact]
        public async Task GetCartItems_Returns_OkObjectResult()
        {
            // Arrange
            var userId = "user123";
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userId)
                    }))
                }
            };

            var cartItems = new List<CartItemDTO>
            {
                new CartItemDTO { Id = 1, ProductId = 101, Quantity = 2 },
                new CartItemDTO { Id = 2, ProductId = 102, Quantity = 1 }
            };

            _mockCartService.Setup(s => s.GetCartItemsAsync(userId)).ReturnsAsync(cartItems);

            // Act
            var result = await _controller.GetCartItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<CartItemDTO>>(okResult.Value);
            Assert.Equal(cartItems.Count, model.Count());
        }

        [Fact]
        public async Task AddCart_Returns_OkResult()
        {
            // Arrange
            var userId = "user123";
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userId)
                    }))
                }
            };

            var addToCartDto = new AddToCartDTO { ProductId = 101, Quantity = 1 };

            // Act
            var result = await _controller.AddCart(addToCartDto);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockCartService.Verify(s => s.AddCartItemAsync(userId, addToCartDto), Times.Once);
        }

        [Fact]
        public async Task UpdateCartItem_Returns_OkResult()
        {
            // Arrange
            var cartItemId = 1;
            var quantity = 3;

            // Act
            var result = await _controller.UpdateCartItem(cartItemId, quantity);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockCartService.Verify(s => s.UpdateCartItemAsync(cartItemId, quantity), Times.Once);
        }

        [Fact]
        public async Task RemoveCartItem_Returns_OkResult()
        {
            // Arrange
            var cartItemId = 1;

            // Act
            var result = await _controller.RemoveCartItem(cartItemId);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockCartService.Verify(s => s.RemoveCartItemAsync(cartItemId), Times.Once);
        }
    }
}
