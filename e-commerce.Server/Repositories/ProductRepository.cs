using e_commerce.Server.Data;
using e_commerce.Server.Repositories.Interfaces;
using e_commerce.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Server.Repositories
{
    public class ProductRepository: RepositoryBase, IProductRepository
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
        public async Task AddProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();  
        }

        public async Task UpdateProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var productToDelete = await _context.Products.FindAsync(id);
            if (productToDelete != null)
            {

                _context.Products.Remove(productToDelete);
                await _context.SaveChangesAsync();
            }

        }


        public async Task<bool> ProductExists(int id)

        {
            return await _context.Products.AnyAsync(e => e.ProductId == id);
        }
    }
}
