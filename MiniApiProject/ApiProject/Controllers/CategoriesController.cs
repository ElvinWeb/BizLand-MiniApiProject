using ApiProject.Business.DTO.CategoryDtos;
using ApiProject.Business.DTO.ProfessionDtos;
using ApiProject.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null && id <= 0) return NotFound();

            CategoryGetDto categoryGetDto = null;
            try
            {
                categoryGetDto = await _categoryService.GetByIdAsync(id);

            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }


            return Ok(categoryGetDto);
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<CategoryGetDto> categoryGetDtos = await _categoryService.GetAllAsync();

            return Ok(categoryGetDtos);
        }
        [HttpPost("")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto categoryCreateDto)
        {
            await _categoryService.CreateAsync(categoryCreateDto);

            return StatusCode(201, new { message = "Object yaradildi" });
        }
        [HttpPut("")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm] CategoryUpdateDto categoryUpdateDto)
        {
            if (categoryUpdateDto.Id == null && categoryUpdateDto.Id <= 0) return NotFound();

            try
            {
                await _categoryService.UpdateAsync(categoryUpdateDto);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }



            return NoContent();
        }
        [HttpDelete("/categories/Delete/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null && id <= 0) return NotFound();
            try
            {
                await _categoryService.DeleteAsync(id);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }


            return NoContent();
        }
        [HttpPatch("/categories/ToggleDelete/{id}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ToggleDelete(int id)
        {
            if (id == null && id <= 0) return NotFound();

            try
            {
                await _categoryService.ToggleDelete(id);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
