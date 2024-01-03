using ApiProject.Business.DTO.CategoryDtos;
using ApiProject.Business.DTO.PortfolioDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.Services
{
    public interface IPortfolioService
    {
        Task CreateAsync([FromForm] PortfolioCreateDto portfolioCreateDto);
        Task UpdateAsync([FromForm] PortfolioUpdateDto portfolioUpdateDto);
        Task DeleteAsync(int id);
        Task ToggleDelete(int id);
        Task<PortfolioGetDto> GetByIdAsync(int id);
        Task<IEnumerable<PortfolioGetDto>> GetAllAsync();
    }
}
