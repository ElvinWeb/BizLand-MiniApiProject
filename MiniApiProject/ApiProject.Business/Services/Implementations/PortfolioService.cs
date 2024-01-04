using ApiProject.Business.CustomExceptions.Common;
using ApiProject.Business.DTO.PortfolioDtos;
using ApiProject.Business.DTO.WorkerDtos;
using ApiProject.Business.Helpers;
using ApiProject.Core.Entites;
using ApiProject.Core.Repositories;
using ApiProject.Data.Repositories.Implementations;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
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
        private readonly ICategoryRepository _categoryRepository;

        public PortfolioService(IPortfolioRepository portfolioRepository,
                                IPortfolioImageRepository portfolioImage,
                                IMapper mapper,
                                IWebHostEnvironment env,
                                ICategoryRepository categoryRepository)
        {
            _portfolioRepository = portfolioRepository;
            _portfolioImage = portfolioImage;
            _mapper = mapper;
            _env = env;
            _categoryRepository = categoryRepository;
        }
        public async Task CreateAsync([FromForm] PortfolioCreateDto portfolioCreateDto)
        {
            if (!_categoryRepository.Table.Any(category => category.Id == portfolioCreateDto.CategoryId))
            {
                throw new NullReferenceException("category not found!");
            }

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
                foreach (var portfolioImg in portfolioCreateDto.PortfolioSlideImages)
                {

                    if (portfolioImg.ContentType != "image/png" && portfolioImg.ContentType != "image/jpeg")
                    {
                        throw new InvalidImageContentTypeOrSize("enter the correct image ContentType!");
                    }

                    if (portfolioImg.Length > 1048576)
                    {
                        throw new InvalidImageContentTypeOrSize("image size must be less than 1mb!");
                    }
                    string folder = "Uploads/PortfolioImages";
                    string newFileName = await Helper.GetFileName(_env.WebRootPath, folder, portfolioImg);

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

        public async Task DeleteAsync(int id)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolio => portfolio.Id == id, "Images");

            if (portfolio == null) throw new NullReferenceException("portfolio couldn't be null!");

            if (portfolio.Images != null)
            {
                foreach (var image in portfolio.Images)
                {
                    string folder = "Uploads/PortfolioImages";

                    if (image.IsPoster == false || image.IsPoster == true)
                    {
                        string path = Path.Combine(_env.WebRootPath, folder, image.ImgUrl);

                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    }

                }
            }

            _portfolioRepository.Delete(portfolio);
            await _portfolioRepository.SaveChanges();
        }

        public async Task<IEnumerable<PortfolioGetDto>> GetAllAsync()
        {
            IEnumerable<Portfolio> portfolios = await _portfolioRepository.GetAllAsync(portfolio => portfolio.IsDeleted == false, "Category", "Images");

            IEnumerable<PortfolioGetDto> workerGetDtos = portfolios.Select(portfolio => new PortfolioGetDto
            {
                Id = portfolio.Id,
                Title = portfolio.Title,
                Description = portfolio.Description,
                Category = portfolio.Category.Name,
                Client = portfolio.Client,
                ProjectDate = portfolio.ProjectDate,
                ProjectUrl = portfolio.ProjectUrl,
                ImgUrl = portfolio.Images.FirstOrDefault(image => image.IsPoster == true).ImgUrl,
            });

            return workerGetDtos;
        }

        public async Task<PortfolioGetDto> GetByIdAsync(int id)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolio => portfolio.Id == id, "Category", "Images");

            if (portfolio == null) throw new NullReferenceException("portfolio couldn't be null!");

            PortfolioGetDto portfolioGetDto = _mapper.Map<PortfolioGetDto>(portfolio);
            portfolioGetDto.Category = portfolio.Category.Name;
            portfolioGetDto.ImgUrl = portfolio.Images.FirstOrDefault(image => image.IsPoster == true).ImgUrl;

            return portfolioGetDto;
        }

        public async Task ToggleDelete(int id)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolio => portfolio.Id == id, "Category");

            if (portfolio == null) throw new NullReferenceException("portfolio couldn't be null!");

            portfolio.IsDeleted = !portfolio.IsDeleted;
            portfolio.DeletedDate = DateTime.UtcNow.AddHours(4);

            await _portfolioRepository.SaveChanges();
        }

        public async Task UpdateAsync([FromForm] PortfolioUpdateDto portfolioUpdateDto)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolio => portfolio.Id == portfolioUpdateDto.Id, "Images");

            if (portfolio == null) throw new NullReferenceException("portfolio couldn't be null!");

            if (!_categoryRepository.Table.Any(category => category.Id == portfolioUpdateDto.CategoryId))
            {
                throw new NullReferenceException("category not found!");
            }

            if (portfolioUpdateDto.PortfolioItemImage != null)
            {
                if (portfolioUpdateDto.PortfolioItemImage.ContentType != "image/png" && portfolioUpdateDto.PortfolioItemImage.ContentType != "image/jpeg")
                {
                    throw new InvalidImageContentTypeOrSize("enter the correct image ContentType!");
                }

                if (portfolioUpdateDto.PortfolioItemImage.Length > 1048576)
                {
                    throw new InvalidImageContentTypeOrSize("image size must be less than 1mb!");
                }

                string folder = "Uploads/PortfolioImages";
                string newFileName = await Helper.GetFileName(_env.WebRootPath, folder, portfolioUpdateDto.PortfolioItemImage);

                string oldImgPath = Path.Combine(_env.WebRootPath, folder, portfolio.Images.FirstOrDefault(img => img.IsPoster == true).ImgUrl);

                if (File.Exists(oldImgPath))
                {
                    File.Delete(oldImgPath);
                }

                PortfolioImage portfolioImage = new PortfolioImage
                {
                    Portfolio = portfolio,
                    ImgUrl = newFileName,
                    IsPoster = true,
                };

                portfolio.Images.Add(portfolioImage);
            }

            if (portfolioUpdateDto.PortfolioSlideImages != null)
            {
                foreach (var img in portfolioUpdateDto.PortfolioSlideImages)
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

                    if (portfolio.Images != null)
                    {
                        foreach (var portfolioImg in portfolio.Images.FindAll(img => img.IsPoster == false))
                        {
                            string oldImgPath = Path.Combine(_env.WebRootPath, folder, portfolioImg.ImgUrl);

                            if (File.Exists(oldImgPath))
                            {
                                File.Delete(oldImgPath);
                            }
                        }
                    }

                    PortfolioImage portfolioImage = new PortfolioImage
                    {
                        Portfolio = portfolio,
                        ImgUrl = newFileName,
                        IsPoster = false,
                    };

                    portfolio.Images.Add(portfolioImage);
                }
            }

            portfolio = _mapper.Map(portfolioUpdateDto, portfolio);

            await _portfolioRepository.SaveChanges();
        }
    }
}
