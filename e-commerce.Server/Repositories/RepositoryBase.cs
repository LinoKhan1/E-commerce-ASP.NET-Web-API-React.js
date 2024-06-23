using e_commerce.Server.Data;
using e_commerce.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Server.Repositories
{
    public abstract class RepositoryBase<T>: IGenericRepository<T> where T : class
    {

        protected readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context)
        {

        _context = context ?? throw new ArgumentNullException(nameof(context)); 
        }

     

        public async Task AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

        }
        public async Task UpdateAsync(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));    
            }
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();  
        }

        public async Task DeleteAsync(int id)
        {
            var entityToDelete = await _context.Set<T>().FindAsync(id);
            if (entityToDelete != null)
            {
                _context.Set<T>().Remove(entityToDelete);
                await _context.SaveChangesAsync();
            }
        }

       /* public async Task<bool> EntityExists(int id)
        {
            return true;
        }*/
    }
}
