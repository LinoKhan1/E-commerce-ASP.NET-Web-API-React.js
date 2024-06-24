using e_commerce.Server.Models;

namespace e_commerce.Server.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {

        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
       
    }


}
