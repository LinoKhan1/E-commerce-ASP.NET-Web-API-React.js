using AutoMapper;
using e_commerce.Server.DTOs;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories.Interfaces;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.uniOfWork;

namespace e_commerce.Server.Services
{
    public class CategoryService: ServiceBase<Category, CategoryDTO>, ICategoryService   
    {
        

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : base(categoryRepository, unitOfWork, mapper    )
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }


        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync(); 

            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);               
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            return _mapper.Map<CategoryDTO>(category);
        }

        protected override async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

    }
}
