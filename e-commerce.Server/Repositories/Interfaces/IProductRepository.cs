using e_commerce.Server.Models;

namespace e_commerce.Server.Repositories.Interfaces
{
    public interface IProductRepository
    {

        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task AddProductAsync (Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);

        Task<bool> ProductExists(int productId);

    }
}
