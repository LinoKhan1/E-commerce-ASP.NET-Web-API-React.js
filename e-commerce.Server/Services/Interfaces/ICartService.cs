using e_commerce.Server.DTOs;

namespace e_commerce.Server.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for a service that manages cart items.
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Gets the cart items for a specified user.
        /// </summary>
        /// <param name="userId">The unique identifier for the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of CartItemDTOs.</returns>
        Task<IEnumerable<CartItemDTO>> GetCartItemsAsync(string userId);

        /// <summary>
        /// Adds a new item to the cart for a specified user.
        /// </summary>
        /// <param name="userId">The unique identifier for the user.</param>
        /// <param name="addToCartDto">The data transfer object containing the details of the item to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddCartItemAsync(string userId, AddToCartDTO addToCartDto);

        /// <summary>
        /// Updates the quantity of an existing cart item.
        /// </summary>
        /// <param name="id">The unique identifier for the cart item.</param>
        /// <param name="quantity">The new quantity for the cart item.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateCartItemAsync(int id, int quantity);

        /// <summary>
        /// Removes a cart item.
        /// </summary>
        /// <param name="id">The unique identifier for the cart item.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RemoveCartItemAsync(int id);
    }
}
