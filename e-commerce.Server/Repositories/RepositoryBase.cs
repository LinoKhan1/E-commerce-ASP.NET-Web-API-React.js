using e_commerce.Server.Data;

namespace e_commerce.Server.Repositories
{
    public abstract class RepositoryBase
    {

        protected readonly ApplicationDbContext _context;

        protected RepositoryBase(ApplicationDbContext context)
        {

        _context = context ?? throw new ArgumentNullException(nameof(context)); 
        }
    }
}
