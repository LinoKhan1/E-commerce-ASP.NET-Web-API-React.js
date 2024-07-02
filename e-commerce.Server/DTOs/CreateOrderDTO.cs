namespace e_commerce.Server.DTOs
{
    public class CreateOrderDTO
    {
        public string UserId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
