namespace e_commerce.Server.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
       // Task<bool> EntityExists(int id);
        
    }
}
