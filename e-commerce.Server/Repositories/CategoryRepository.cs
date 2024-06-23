using e_commerce.Server.Data;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Server.Repositories
{
    public class CategoryRepository: RepositoryBase<Category>, ICategoryRepository
    {

        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context) 
        { 
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

      


        /*public async Task<bool> CategoryExists(int id)

        {
            return await _context.Categories.AnyAsync(e => e.CategoryId == id);
        }

        */
    }
}
