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
    }
}
