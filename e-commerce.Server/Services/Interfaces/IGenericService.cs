namespace e_commerce.Server.Services.Interfaces
{
    public interface IGenericService<TDto>
    {
     
        Task AddAsync(TDto dto);
        Task UpdateAsync(TDto dto);

        Task DeleteAsync(int id);
       
        //Task<bool> EntityExists(int id);
    }
}
