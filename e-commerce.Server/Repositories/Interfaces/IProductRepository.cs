using e_commerce.Server.Models;

namespace e_commerce.Server.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
      
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);

        //Task DeleteProductAsync(int productId); 

    }
}
