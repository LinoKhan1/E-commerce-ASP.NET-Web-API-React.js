using e_commerce.Server.Models;
using e_commerce.Server.Repositories;
using e_commerce.Server.Repositories.Interfaces;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.uniOfWork;

namespace e_commerce.Server.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetCategoryByIdAsync(categoryId);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _categoryRepository.AddCategoryAsync(category);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _categoryRepository.UpdateCategoryAsync(category);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {

            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category != null)
            {
                _categoryRepository.DeleteCategoryAsync(category.CategoryId);
                await _unitOfWork.CompleteAsync();
            }

        }

        public async Task<bool> CategoryExists(int id)
        {
            return await _categoryRepository.CategoryExists(id);
        }


    }
}
