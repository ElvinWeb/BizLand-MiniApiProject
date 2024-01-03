using ApiProject.Business.DTO.PortfolioDtos;
using ApiProject.Business.DTO.WorkerDtos;
using ApiProject.Core.Entites;
using ApiProject.Core.Repositories;
using ApiProject.Data.Repositories.Implementations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.Services.Implementations
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IMapper _mapper;

        public PortfolioService(IPortfolioRepository portfolioRepository, IMapper mapper)
        {
            _portfolioRepository = portfolioRepository;
            _mapper = mapper;
        }
        public Task CreateAsync([FromForm] PortfolioCreateDto portfolioCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PortfolioGetDto>> GetAllAsync()
        {
            IEnumerable<Portfolio> portfolios = await _portfolioRepository.GetAllAsync(portfolio => portfolio.IsDeleted == false, "Category");

            IEnumerable<PortfolioGetDto> workerGetDtos = portfolios.Select(portfolio => new PortfolioGetDto { Id = portfolio.Id , Category = portfolio.Category.Name , 
                                                                                                              Client = portfolio.Client , Title = portfolio.Title , 
                                                                                                              Description = portfolio.Description  , ProjectDate = portfolio.ProjectDate ,
                                                                                                              ProjectUrl = portfolio.ProjectUrl });

            return workerGetDtos;
        }

        public async Task<PortfolioGetDto> GetByIdAsync(int id)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolio => portfolio.Id == id, "Category");

            if (portfolio == null) throw new NullReferenceException("portfolio couldn't be null!");

            PortfolioGetDto portfolioGetDto = _mapper.Map<PortfolioGetDto>(portfolio);
            portfolioGetDto.Category = portfolio.Category.Name;

            return portfolioGetDto;
        }

        public Task ToggleDelete(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync([FromForm] PortfolioUpdateDto portfolioUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
