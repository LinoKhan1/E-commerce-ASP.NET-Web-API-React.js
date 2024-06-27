using AutoMapper;
using e_commerce.Server.DTOs;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories.Interfaces;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.uniOfWork;

namespace e_commerce.Server.Services
{
    /// <summary>
    /// Provides methods to manage cart items.
    /// </summary>
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        /// <param name="cartRepository">The cart repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public CartService(ICartRepository cartRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _unitOfWork  = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the cart items for a specified user.
        /// </summary>
        /// <param name="userId">The unique identifier for the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of CartItemDTOs.</returns>
        public async Task<IEnumerable<CartItemDTO>> GetCartItemsAsync(string userId)
        {
            var cartItems = await _cartRepository.GetCartItemsAsync(userId);
            return _mapper.Map<IEnumerable<CartItemDTO>>(cartItems);
        }

        /// <summary>
        /// Adds a new item to the cart for a specified user.
        /// </summary>
        /// <param name="userId">The unique identifier for the user.</param>
        /// <param name="addToCartDto">The data transfer object containing the details of the item to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddCartItemAsync(string userId, AddToCartDTO addToCartDto)
        {
            var cartItem = new CartItem
            {
                ProductId = addToCartDto.ProductId,
                UserId = userId,
                Quantity = addToCartDto.Quantity,
                DateAdded = DateTime.UtcNow,
            };

            await _cartRepository.AddAsync(cartItem);
            await _unitOfWork.CompleteAsync();  
        }

        /// <summary>
        /// Updates the quantity of an existing cart item.
        /// </summary>
        /// <param name="id">The unique identifier for the cart item.</param>
        /// <param name="quantity">The new quantity for the cart item.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task UpdateCartItemAsync(int id, int quantity)
        {
            var cartItem = await _cartRepository.GetCartItemByIdAsync(id);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                await _cartRepository.UpdateAsync(cartItem);
                await _unitOfWork.CompleteAsync();

            }
        }

        /// <summary>
        /// Removes a cart item.
        /// </summary>
        /// <param name="id">The unique identifier for the cart item.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task RemoveCartItemAsync(int id)
        {
            await _cartRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();

        }
    }
}
