using AutoMapper;
using e_commerce.Server.DTOs;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories.Interfaces;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.uniOfWork;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace e_commerce.Server.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartService _cartService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService (IOrderRepository orderRepository, ICartService cartService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartService = cartService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 
        
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
            
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }

        public async Task CreateOrderAsync(string userId, IEnumerable<CartItemDTO> cartItems)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                OrderItems = cartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Price
                }).ToList(),
                TotalAmount = cartItems.Sum(ci => ci.Price * ci.Quantity),
                PaymentStatus = "Pending"
            };

            await _orderRepository.AddOrderAsync(order);
            await _unitOfWork.CompleteAsync();
        }
    }
}
