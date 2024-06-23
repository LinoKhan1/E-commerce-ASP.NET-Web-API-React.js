using e_commerce.Server.DTOs;
using e_commerce.Server.Models;

namespace e_commerce.Server.Services.Interfaces
{
    public interface ICategoryService : IGenericService<CategoryDTO>
    {

        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int categoeyId);
        
    }
}
