namespace e_commerce.Server.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object for a cart item.
    /// </summary>
    public class CartItemDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the cart item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in the cart.
        /// </summary>
        public int Quantity { get; set; }
    }
}
