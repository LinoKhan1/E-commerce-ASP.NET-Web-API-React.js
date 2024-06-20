using e_commerce.Server.Data;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Server.Repositories
{
    public class CategoryRepository: RepositoryBase, ICategoryRepository
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

        public async Task AddCategoryAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCategoryAsync(Category category)
        {
            if (category == null)
            {

                throw new ArgumentNullException(nameof(category));
            }
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }


        public async Task DeleteCategoryAsync(int id)
        {
            var categoryToDelete = await _context.Categories.FindAsync(id);
            if (categoryToDelete != null)
            {

                _context.Categories.Remove(categoryToDelete);
                await _context.SaveChangesAsync();
            }

        }


        public async Task<bool> CategoryExists(int id)

        {
            return await _context.Categories.AnyAsync(e => e.CategoryId == id);
        }
    }
}
