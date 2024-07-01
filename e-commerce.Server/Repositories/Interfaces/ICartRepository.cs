using e_commerce.Server.Models;

namespace e_commerce.Server.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for a repository that manages cart items.
    /// </summary>
    public interface ICartRepository
    {
        /// <summary>
        /// Gets the cart items for a specified user.
        /// </summary>
        /// <param name="userId">The unique identifier for the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of CartItems.</returns>
        Task<IEnumerable<CartItem>> GetCartItemsAsync(string userId);

        /// <summary>
        /// Gets a cart item by its unique identifier.
        /// </summary>
        /// <param name="cartItemId">The unique identifier for the cart item.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the CartItem if found; otherwise, null.</returns>
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);

        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int cartItemId);





    }
}
