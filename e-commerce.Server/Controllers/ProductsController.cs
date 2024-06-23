using e_commerce.Server.DTOs;
using e_commerce.Server.Models;
using e_commerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace e_commerce.Server.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
       
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);    
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);    
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productDto)
        {
            await _productService.AddAsync(productDto);
            return CreatedAtAction(nameof(GetProduct), new { id = productDto.ProductId }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO productDTO)
        {
            if (id != productDTO.ProductId)
            {
                return BadRequest();
            }
            await _productService.UpdateAsync(productDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
