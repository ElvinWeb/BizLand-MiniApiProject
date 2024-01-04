using ApiProject.Business.DTO.ProfessionDtos;
using ApiProject.Business.DTO.ServiceDtos;
using ApiProject.Business.Services;
using ApiProject.Business.Services.Implementations;
using ApiProject.Core.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionsController : ControllerBase
    {
        private readonly IProfessionService _professionService;

        public ProfessionsController(IProfessionService professionService)
        {
            _professionService = professionService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null && id <= 0) return NotFound();

            ProfessionGetDto professionGetDto = null;
            try
            {
                professionGetDto = await _professionService.GetByIdAsync(id);

            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }


            return Ok(professionGetDto);
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ProfessionGetDto> professionGetDtos = await _professionService.GetAllAsync();

            return Ok(professionGetDtos);
        }
        [HttpPost("")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm] ProfessionCreateDto professionCreateDto)
        {
            await _professionService.CreateAsync(professionCreateDto);

            return StatusCode(201, new { message = "Object yaradildi" });
        }
        [HttpPut("")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm] ProfessionUpdateDto professionUpdateDto)
        {
            if (professionUpdateDto.Id == null && professionUpdateDto.Id <= 0) return NotFound();

            try
            {
                await _professionService.UpdateAsync(professionUpdateDto);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }
        [HttpDelete("/professions/Delete/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null && id <= 0) return NotFound();
            try
            {
                await _professionService.DeleteAsync(id);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }


            return NoContent();
        }
        [HttpPatch("/professions/ToggleDelete/{id}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ToggleDelete(int id)
        {
            if (id == null && id <= 0) return NotFound();

            try
            {
                await _professionService.ToggleDelete(id);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
