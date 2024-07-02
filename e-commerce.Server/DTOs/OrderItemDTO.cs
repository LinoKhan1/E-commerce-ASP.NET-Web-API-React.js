namespace e_commerce.Server.DTOs
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
