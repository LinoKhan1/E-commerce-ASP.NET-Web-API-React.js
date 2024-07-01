using e_commerce.Server.Data;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Server.Repositories
{
    /// <summary>
    /// Provides a repository for managing cart items.
    /// </summary>
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public CartRepository(ApplicationDbContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the cart items for a specified user.
        /// </summary>
        /// <param name="userId">The unique identifier for the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of CartItems.</returns>
        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(string userId)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.UserId == userId)
                .ToListAsync();
        }

        /// <summary>
        /// Gets a cart item by its unique identifier.
        /// </summary>
        /// <param name="cartItemId">The unique identifier for the cart item.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the CartItem if found; otherwise, null.</returns>
        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId);
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _context.Entry(cartItem).State = EntityState.Modified;  
        }

        public async Task DeleteCartItemAsync(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
               
            }
        }

    }
}
