using e_commerce.Server.Data;
using e_commerce.Server.Repositories;
using e_commerce.Server.Repositories.Interfaces;
using Microsoft.Identity.Client;

namespace e_commerce.Server.uniOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;

        private readonly ApplicationDbContext _context;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IProductRepository ProductRepository
        {
            get
            {

                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_context);
                }
                return _productRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_context);
                }
                return _categoryRepository;
            }
        }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
