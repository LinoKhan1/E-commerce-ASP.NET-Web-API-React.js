using e_commerce.Server.DTOs;
using e_commerce.Server.Models;

namespace e_commerce.Server.Services.Interfaces
{
    public interface IProductService: IGenericService<ProductDTO>
    {

        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int productId);

      
    }
}
