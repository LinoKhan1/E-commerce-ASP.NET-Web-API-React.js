using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce.Server.DTOs;
using e_commerce.Server.Models;
using e_commerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace e_commerce.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ILogger<CheckoutController> _logger;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;

        public CheckoutController(
            ILogger<CheckoutController> logger,
            ICartService cartService,
            IOrderService orderService,
            IPaymentService paymentService)
        {
            _logger = logger;
            _cartService = cartService;
            _orderService = orderService;
            _paymentService = paymentService;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDto)
        {
            try
            {
                var userId = "user_id"; // Replace with actual user id from authentication
                var cartItems = await _cartService.GetCartItemsAsync(userId);

                if (cartItems == null || !cartItems.Any())
                {
                    return BadRequest("Cart is empty. Cannot create an order.");
                }

                await _orderService.CreateOrderAsync(userId, cartItems);

                return Ok("Order created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order.");
                return StatusCode(500, "Error creating order. Please try again later.");
            }
        }

        [HttpPost("initiate-payment")]
        public async Task<IActionResult> InitiatePayment([FromBody] CreateOrderDTO createOrderDto)
        {
            try
            {
                var userId = "user_id"; // Replace with actual user id from authentication
                var cartItems = await _cartService.GetCartItemsAsync(userId);

                if (cartItems == null || !cartItems.Any())
                {
                    return BadRequest("Cart is empty. Cannot initiate payment.");
                }

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

                var approvalUrl = await _paymentService.CreatePayment(order);

                return Ok(new { ApprovalUrl = approvalUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initiating payment.");
                return StatusCode(500, "Error initiating payment. Please try again later.");
            }
        }
    }
}
