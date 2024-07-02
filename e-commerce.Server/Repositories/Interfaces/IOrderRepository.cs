using e_commerce.Server.Models;

namespace e_commerce.Server.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Gets the order by its identifier.
        /// </summary>
        /// <param name="orderId">The unique identifier for the order.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Order.</returns>
        Task<Order> GetOrderByIdAsync(int orderId);

        /// <summary>
        /// Gets all orders for a specified user.
        /// </summary>
        /// <param name="userId">The unique identifier for the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of Orders.</returns>
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);

        /// <summary>
        /// Adds a new order.
        /// </summary>
        /// <param name="order">The order to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddOrderAsync(Order order);

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="order">The order to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateOrderAsync(Order order);
    }
}
