using e_commerce.Server.Repositories.Interfaces;

namespace e_commerce.Server.uniOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task<int> CompleteAsync();


    }
}
