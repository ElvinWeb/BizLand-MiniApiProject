using ApiProject.Business.CustomExceptions.Common;
using ApiProject.Business.DTO.PortfolioDtos;
using ApiProject.Business.DTO.WorkerDtos;
using ApiProject.Business.Services;
using ApiProject.Business.Services.Implementations;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;

        public PortfoliosController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null && id <= 0) return NotFound();

            PortfolioGetDto portfolioGetDto = null;

            try
            {
                portfolioGetDto = await _portfolioService.GetByIdAsync(id);

            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(portfolioGetDto);
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<PortfolioGetDto> portfolioGetDtos = await _portfolioService.GetAllAsync();

            return Ok(portfolioGetDtos);
        }
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm] PortfolioCreateDto portfolioCreateDto)
        {
            try
            {
                await _portfolioService.CreateAsync(portfolioCreateDto);
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromForm] PortfolioUpdateDto portfolioUpdateDto)
        {
            if (portfolioUpdateDto.Id == null && portfolioUpdateDto.Id <= 0) return NotFound();

            try
            {
                await _portfolioService.UpdateAsync(portfolioUpdateDto);

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
        [HttpDelete("/portfolios/Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null && id <= 0) return NotFound();

            try
            {
                await _portfolioService.DeleteAsync(id);

            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
        [HttpPatch("/portfolios/ToggleDelete/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ToggleDelete(int id)
        {
            if (id == null && id <= 0) return NotFound();

            try
            {
                await _portfolioService.ToggleDelete(id);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
