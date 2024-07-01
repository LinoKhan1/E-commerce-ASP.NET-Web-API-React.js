using AutoMapper;
using e_commerce.Server.DTOs;
using e_commerce.Server.Mappings;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories.Interfaces;
using e_commerce.Server.Services;
using e_commerce.Server.uniOfWork;
using Moq;
using Xunit;

namespace e_commerce.tests.Services
{
    public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _mockRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public CartServiceTests()
        {
            // Setup AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();  

            // Setup Mock Repository
            _mockRepository = new Mock<ICartRepository>();

            // Setup Mock UnitOfWork
            _mockUnitOfWork = new Mock<IUnitOfWork>();


        }

        [Fact]
        public async Task GetCartItemsAsync_ReturnsCartItems()
        {

            // Arrange
            string userId = "testUserId";
            var expectedCartItems = new List<CartItem>
            {
                new CartItem {CartItemId = 1, UserId = userId, ProductId = 1, Quantity = 2,
                    Product = new Product{ProductId = 1, Name = "Product 1", Price= 10.99m} },
                new CartItem {CartItemId = 2, UserId = userId, ProductId = 2, Quantity = 1,
                    Product = new Product{ProductId = 2, Name = "Product 2", Price= 10.99m} },
 
            };

            _mockRepository.Setup(repo => repo.GetCartItemsAsync(userId))
                .ReturnsAsync(expectedCartItems);

            var cartService = new CartService(_mockRepository.Object, _mockUnitOfWork.Object, _mapper);

            // Act 
            var result = await cartService.GetCartItemsAsync(userId);

            // Assert
            // Assert
            _mockRepository.Verify(repo => repo.GetCartItemsAsync(userId), Times.Once);

            // Additional assertions based on DTO mappings
            Assert.NotNull(result);
            var resultList = result.ToList();
            Assert.Equal(expectedCartItems.Count, resultList.Count);

            // Verify mapping correctness
            for (int i = 0; i < expectedCartItems.Count; i++)
            {
                Assert.Equal(expectedCartItems[i].ProductId, resultList[i].ProductId);
                Assert.Equal(expectedCartItems[i].Product.Name, resultList[i].ProductName); // Accessing ProductName through navigation property
                Assert.Equal(expectedCartItems[i].Product.Price, resultList[i].Price); // Accessing Price through navigation property
                Assert.Equal(expectedCartItems[i].Quantity, resultList[i].Quantity);
            }



        }

        [Fact]
        public async Task AddCartItemAsync_AddsCartItem()
        {
            // Arrange
            string userId = "testUserId";
            var addToCartDto = new AddToCartDTO { ProductId = 1, Quantity = 2 };
            var cartService = new CartService(_mockRepository.Object, _mockUnitOfWork.Object, _mapper);

            // Act
            await cartService.AddCartItemAsync(userId, addToCartDto);

            // Assert
            _mockRepository.Verify(repo => repo.AddCartItemAsync(It.IsAny<CartItem>()), Times.Once());
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateCartItemAsync_UpdatesCartItemQuantity()
        {
            // Arrange
            int cartItemId = 1;
            int newQuantity = 3;

            var cartItem = new CartItem { CartItemId = cartItemId, Quantity = 2 };
            _mockRepository.Setup(repo => repo.GetCartItemByIdAsync(cartItemId))
                              .ReturnsAsync(cartItem);
            var cartService = new CartService(_mockRepository.Object, _mockUnitOfWork.Object, _mapper);

            // Act
            await cartService.UpdateCartItemAsync(cartItemId, newQuantity);

            // Assert
            Assert.Equal(newQuantity, cartItem.Quantity);
            _mockRepository.Verify(repo => repo.UpdateCartItemAsync(cartItem), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveCartItemAsync_RemovesCartItem()
        {
            // Arrange
            int cartItemId = 1;
            var cartService = new CartService(_mockRepository.Object, _mockUnitOfWork.Object, _mapper);

            // Act
            await cartService.RemoveCartItemAsync(cartItemId);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteCartItemAsync(cartItemId), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Once);
        }
    }
}
