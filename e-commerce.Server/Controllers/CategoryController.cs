using e_commerce.Server.DTOs;
using e_commerce.Server.Models;
using e_commerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Server.Controllers
{
    /// <summary>
    /// API Controller for managing categories.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;



        /// <summary>
        /// Constructor for CategoryController.
        /// </summary>
        /// <param name="categoryService">The category service instance.</param>
        public CategoryController(ICategoryService categoryService)
        {

            _categoryService = categoryService;

        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of all categories.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>The category with the specified ID.</returns>
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

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="categoryDTO">The category to add.</param>
        /// <returns>The newly created category.</returns>
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO categoryDTO)
        {
            await _categoryService.AddAsync(categoryDTO);
            return CreatedAtAction(nameof(GetCategory), new { id = categoryDTO.CategoryId }, categoryDTO);
        }


        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryDTO">The updated category information.</param>
        /// <returns>No content.</returns>
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


        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
       
    }
}
