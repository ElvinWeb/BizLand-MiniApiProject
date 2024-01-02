using ApiProject.Business.DTO.ProfessionDtos;
using ApiProject.Business.DTO.ServiceDtos;
using ApiProject.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeaturesController(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null && id <= 0) return NotFound();

            FeatureGetDto featureGetDto = null;
            try
            {
                featureGetDto = await _featureService.GetByIdAsync(id);

            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(featureGetDto);
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<FeatureGetDto> featureGetDtos = await _featureService.GetAllAsync();

            return Ok(featureGetDtos);
        }
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm] FeatureCreateDto featureCreateDto)
        {
            await _featureService.CreateAsync(featureCreateDto);

            return StatusCode(201, new { message = "Object yaradildi" });
        }
        [HttpPut("")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm] FeatureUpdateDto featureUpdateDto)
        {
            if (featureUpdateDto.Id == null && featureUpdateDto.Id <= 0) return NotFound();

            try
            {
                await _featureService.UpdateAsync(featureUpdateDto);

            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
        [HttpDelete("/features/Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null && id <= 0) return NotFound();

            try
            {
                await _featureService.DeleteAsync(id);

            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
        [HttpDelete("/features/ToggleDelete/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ToggleDelete(int id)
        {
            if (id == null && id <= 0) return NotFound();

            try
            {
                await _featureService.ToggleDelete(id);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
