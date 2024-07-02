using e_commerce.Server.DTOs;
using e_commerce.Server.Models;

namespace e_commerce.Server.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(int orderId);

        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task CreateOrderAsync(string userId, IEnumerable<CartItemDTO> cartItems);
    }
}
