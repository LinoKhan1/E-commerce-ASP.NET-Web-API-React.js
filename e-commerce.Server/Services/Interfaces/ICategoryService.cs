using e_commerce.Server.DTOs;

namespace e_commerce.Server.Services.Interfaces
{
    public interface ICategoryService : IGenericService<CategoryDTO>
    {

        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int categoeyId);
        
    }
}
