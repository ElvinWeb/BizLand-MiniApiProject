using ApiProject.Business.CustomExceptions.Common;
using ApiProject.Business.DTO.PortfolioDtos;
using ApiProject.Business.DTO.WorkerDtos;
using ApiProject.Business.Helpers;
using ApiProject.Core.Entites;
using ApiProject.Core.Repositories;
using ApiProject.Data.Repositories.Implementations;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IPortfolioImageRepository _portfolioImage;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public PortfolioService(IPortfolioRepository portfolioRepository, IPortfolioImageRepository portfolioImage, IMapper mapper, IWebHostEnvironment env)
        {
            _portfolioRepository = portfolioRepository;
            _portfolioImage = portfolioImage;
            _mapper = mapper;
            _env = env;
        }
        public async Task CreateAsync([FromForm] PortfolioCreateDto portfolioCreateDto)
        {
            Portfolio portfolio = _mapper.Map<Portfolio>(portfolioCreateDto);

            if (portfolioCreateDto.PortfolioItemImage != null)
            {

                if (portfolioCreateDto.PortfolioItemImage.ContentType != "image/png" && portfolioCreateDto.PortfolioItemImage.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentTypeOrSize("enter the correct image ContentType!");
                }

                if (portfolioCreateDto.PortfolioItemImage.Length > 1048576)
                {
                    throw new InvalidImageContentTypeOrSize("image size must be less than 1mb!");
                }

                string folder = "Uploads/PortfolioImages";

                string newFileName = await Helper.GetFileName(_env.WebRootPath, folder, portfolioCreateDto.PortfolioItemImage);
                PortfolioImage portfolioImage = new PortfolioImage
                {
                    Portfolio = portfolio,
                    ImgUrl = newFileName,
                    IsPoster = true,
                };

                await _portfolioImage.CreateAsync(portfolioImage);
            }
            else
            {
                throw new InvalidImage("Image is required!");
            }

            if (portfolioCreateDto.PortfolioSlideImages != null)
            {
                foreach (var img in portfolioCreateDto.PortfolioSlideImages)
                {

                    if (img.ContentType != "image/png" && img.ContentType != "image/jpeg")
                    {
                        throw new InvalidImageContentTypeOrSize("enter the correct image ContentType!");
                    }

                    if (img.Length > 1048576)
                    {
                        throw new InvalidImageContentTypeOrSize("image size must be less than 1mb!");
                    }
                    string folder = "Uploads/PortfolioImages";
                    string newFileName = await Helper.GetFileName(_env.WebRootPath, folder, img);

                    PortfolioImage portfolioImage = new PortfolioImage
                    {
                        Portfolio = portfolio,
                        ImgUrl = newFileName,
                        IsPoster = false,
                    };

                    await _portfolioImage.CreateAsync(portfolioImage);
                }
            }

            await _portfolioRepository.CreateAsync(portfolio);
            await _portfolioRepository.SaveChanges();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PortfolioGetDto>> GetAllAsync()
        {
            IEnumerable<Portfolio> portfolios = await _portfolioRepository.GetAllAsync(portfolio => portfolio.IsDeleted == false, "Category");

            IEnumerable<PortfolioGetDto> workerGetDtos = portfolios.Select(portfolio => new PortfolioGetDto
            {
                Id = portfolio.Id,
                Category = portfolio.Category.Name,
                Client = portfolio.Client,
                Title = portfolio.Title,
                Description = portfolio.Description,
                ProjectDate = portfolio.ProjectDate,
                ProjectUrl = portfolio.ProjectUrl
            });

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
