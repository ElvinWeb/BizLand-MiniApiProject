using ApiProject.Business.CustomExceptions.Common;
using ApiProject.Business.DTO.ServiceDtos;
using ApiProject.Business.DTO.WorkerDtos;
using ApiProject.Business.Services;
using ApiProject.Business.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkersController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null && id <= 0) return NotFound();

            WorkerGetDto workerGetDto = null;
            try
            {
                workerGetDto = await _workerService.GetByIdAsync(id);

            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(workerGetDto);
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<WorkerGetDto> workerGetDtos = await _workerService.GetAllAsync();

            return Ok(workerGetDtos);
        }
        [HttpPost("")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm] WorkerCreateDto workerCreateDto)
        {
            try
            {

                await _workerService.CreateAsync(workerCreateDto);
            }
            catch (InvalidImageContentTypeOrSize ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidImage ex)
            {
                return NotFound(ex.Message);
            }

            return StatusCode(201, new { message = "Object yaradildi" });
        }
        [HttpPut("")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm] WorkerUpdateDto workerUpdateDto)
        {
            if (workerUpdateDto.Id == null && workerUpdateDto.Id <= 0) return NotFound();

            try
            {
                await _workerService.UpdateAsync(workerUpdateDto);

            }
            catch (InvalidImageContentTypeOrSize ex)
            {
                return NotFound(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
        [HttpDelete("/workers/Delete/{id}")]
        [Authorize(Roles = "SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null && id <= 0) return NotFound();

            try
            {
                await _workerService.DeleteAsync(id);

            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
        [HttpPatch("/workers/ToggleDelete/{id}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ToggleDelete(int id)
        {
            if (id == null && id <= 0) return NotFound();

            try
            {
                await _workerService.ToggleDelete(id);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
