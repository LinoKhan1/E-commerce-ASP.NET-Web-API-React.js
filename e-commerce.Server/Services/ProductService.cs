using e_commerce.Server.Models;
using e_commerce.Server.Repositories.Interfaces;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.uniOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;


namespace e_commerce.Server.Services
{
    public class ProductService: IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;   
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetProductByIdAsync(productId); 
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _productRepository.UpdateProductAsync(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {

            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product != null)
            {
                _productRepository.DeleteProductAsync(product.ProductId);
                await _unitOfWork.CompleteAsync();
            }

        }

        public async Task<bool> ProductExists(int id)
        {
            return await _productRepository.ProductExists(id);
        }


    }
}
