using e_commerce.Server.DTOs;
using e_commerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;


namespace e_commerce.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemDTO>>> GetCartItems()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                var cartItems = await _cartService.GetCartItemsAsync(userId);
                return Ok(cartItems);

            }
            else
            {
                return BadRequest("User ID not found in claims.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] AddToCartDTO addToCartDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                await _cartService.AddCartItemAsync(userId, addToCartDto);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] int quantity)
        {
            await _cartService.UpdateCartItemAsync(id, quantity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCartItem(int id)
        {
            await _cartService.RemoveCartItemAsync(id);
            return Ok();
        }

    }
}
