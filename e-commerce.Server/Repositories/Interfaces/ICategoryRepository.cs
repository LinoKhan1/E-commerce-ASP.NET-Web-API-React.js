using e_commerce.Server.Models;

namespace e_commerce.Server.Repositories.Interfaces
{
    public interface ICategoryRepository
    {

        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<Category> GetCategoryByIdAsync(int categoeyId);
        Task AddCategoryAsync (Category category);
        Task UpdateCategoryAsync (Category category);
        Task DeleteCategoryAsync(int categoryId);
        Task<bool> CategoryExists(int categoryId);  
    }


}
