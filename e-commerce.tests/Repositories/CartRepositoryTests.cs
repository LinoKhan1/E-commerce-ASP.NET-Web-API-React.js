using e_commerce.Server.Data;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories;
using e_commerce.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace e_commerce.Tests.Repositories
{
    /// <summary>
    /// Provides unit tests for the <see cref="CartRepository"/> class.
    /// </summary>
    public class CartRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartRepository _cartRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartRepositoryTests"/> class.
        /// </summary>
        public CartRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _cartRepository = new CartRepository(_context);

            SeedDatabase();
        }

        /// <summary>
        /// Seeds the in-memory database with test data.
        /// </summary>
        private void SeedDatabase()
        {
            if (!_context.CartItems.Any())
            {
              
                var products = new List<Product>
                {
                    new Product { ProductId = 101, Name = "Product1", Description="Test Description", Stock=50, Price = 100 },
                    new Product { ProductId = 102, Name = "Product2", Description="Test Description", Stock=50, Price = 200 }
                };
                _context.Products.AddRange(products);

                _context.CartItems.AddRange(
                    new CartItem
                    {
                        CartItemId = 1,
                        UserId = "user1",
                        ProductId = 101,
                        Quantity = 2,
                        DateAdded = DateTime.UtcNow,
                        Product = products[0]
                    },
                    new CartItem
                    {
                        CartItemId = 2,
                        UserId = "user2",
                        ProductId = 102,
                        Quantity = 1,
                        DateAdded = DateTime.UtcNow,
                        Product = products[1]
                    }
                );
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Tests that the GetCartItemsAsync method returns cart items for a specified user.
        /// </summary>

        [Fact]
        public async Task GetCartItemsAsync_ReturnsCartItemsForUser()
        {
            // Act
            var cartItems = await _cartRepository.GetCartItemsAsync("user1");

            // Assert
            Assert.NotNull(cartItems);
            var cartItemList = cartItems.ToList();
            Assert.Single(cartItemList); // Assuming one item for user1
            Assert.Equal(1, cartItemList[0].CartItemId);
            Assert.Equal("user1", cartItemList[0].UserId);
            Assert.Equal(101, cartItemList[0].ProductId);
            Assert.Equal(2, cartItemList[0].Quantity);
            Assert.NotNull(cartItemList[0].Product);
            Assert.Equal(101, cartItemList[0].Product.ProductId);
            Assert.Equal("Product1", cartItemList[0].Product.Name);
            Assert.Equal(100, cartItemList[0].Product.Price);
        }


        /// <summary>
        /// Tests that the GetCartItemByIdAsync method returns a cart item by its unique identifier.
        /// </summary>
        [Fact]
        public async Task GetCartItemByIdAsync_ReturnsCartItemById()
        {
            // Act
            var cartItem = await _cartRepository.GetCartItemByIdAsync(1);

            // Assert
            Assert.NotNull(cartItem);
            Assert.Equal(1, cartItem.CartItemId);
            Assert.Equal("user1", cartItem.UserId);
            Assert.Equal(101, cartItem.ProductId);
            Assert.Equal(2, cartItem.Quantity);
        }

        /// <summary>
        /// Disposes of the resources used by the test.
        /// </summary>
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
