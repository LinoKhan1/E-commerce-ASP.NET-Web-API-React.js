using e_commerce.Server.DTOs;
using e_commerce.Server.Models;
using e_commerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Server.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {

            _categoryService = categoryService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id); 
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);

        }
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO categoryDTO)
        {
            await _categoryService.AddAsync(categoryDTO);
            return CreatedAtAction(nameof(GetCategory), new { id = categoryDTO.CategoryId }, categoryDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
            {
                return BadRequest();
            }
            await _categoryService.UpdateAsync(categoryDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
       
    }
}
