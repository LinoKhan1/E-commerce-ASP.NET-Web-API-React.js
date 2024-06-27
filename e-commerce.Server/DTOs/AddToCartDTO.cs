namespace e_commerce.Server.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object for adding an item to the cart.
    /// </summary>
    public class AddToCartDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product to be added to the cart.
        /// </summary>
        public int Quantity { get; set; }
    }
}
