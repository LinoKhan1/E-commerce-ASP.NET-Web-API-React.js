using AutoMapper;
using e_commerce.Server.DTOs;
using e_commerce.Server.Models;
using e_commerce.Server.Repositories.Interfaces;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.uniOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;


namespace e_commerce.Server.Services
{
    public class ProductService: ServiceBase<Product, ProductDTO>, IProductService
    {

      
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : base (productRepository, unitOfWork, mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;

        }
         public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            return _mapper.Map<ProductDTO>(product);
                
        }

        protected override async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }
    }
}
