namespace e_commerce.Server.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public int Stock {  get; set; } 
        public int CategoryId   { get; set; }
    }
}
