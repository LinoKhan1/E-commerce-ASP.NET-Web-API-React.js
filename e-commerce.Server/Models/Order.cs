using System.Diagnostics.Contracts;

namespace e_commerce.Server.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
    }
}
