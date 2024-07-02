using e_commerce.Server.Repositories.Interfaces;

namespace e_commerce.Server.uniOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        ICartRepository CartRepository { get; }

        IOrderRepository OrderRepository { get; }
        Task<int> CompleteAsync();


    }
}
