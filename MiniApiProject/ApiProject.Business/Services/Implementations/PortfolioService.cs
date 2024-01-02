using ApiProject.Business.DTO.PortfolioDtos;
using ApiProject.Core.Repositories;
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

        public Task<IEnumerable<PortfolioUpdateDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PortfolioUpdateDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
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
