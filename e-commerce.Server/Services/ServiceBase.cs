using AutoMapper;
using e_commerce.Server.Repositories.Interfaces;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.uniOfWork;

namespace e_commerce.Server.Services
{
    public abstract class ServiceBase<TEntity, TDto> : IGenericService<TDto> where TEntity : class 
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceBase(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }


        public async Task AddAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                await _repository.DeleteAsync(id); // Ensure you await the deletion
                await _unitOfWork.CompleteAsync(); // Save changes
            }
            else
            {
                throw new ArgumentException($"Entity with ID {id} not found.");
            }

        }
        protected abstract Task<TEntity> GetByIdAsync(int id);

    }
    
}
