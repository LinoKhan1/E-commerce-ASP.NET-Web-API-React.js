using e_commerce.Server.Data;
using e_commerce.Server.Repositories.Interfaces;
using e_commerce.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Server.Repositories
{
    public class ProductRepository: RepositoryBase<Product>, IProductRepository 
    {

        private readonly ApplicationDbContext _context;


        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

       public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
        }
    }
}
