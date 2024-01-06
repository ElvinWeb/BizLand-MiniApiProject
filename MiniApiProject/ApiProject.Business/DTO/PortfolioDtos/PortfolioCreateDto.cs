using ApiProject.Business.DTO.ServiceDtos;
using ApiProject.Core.Entites;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.DTO.PortfolioDtos
{
    public class PortfolioCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectUrl { get; set; }
        public string Client { get; set; }
        public string ProjectDate { get; set; }
        public int CategoryId { get; set; }
        public List<int>? PortfolioImageIds { get; set; }
        public List<IFormFile> PortfolioSlideImages { get; set; }
        public IFormFile PortfolioItemImage { get; set; }
    }

    public class PortfolioCreateDtoValidator : AbstractValidator<PortfolioCreateDto>
    {
        public PortfolioCreateDtoValidator()
        {
            RuleFor(portfolio => portfolio.Title)
               .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!")
               .MaximumLength(20).WithMessage("Max 20 ola biler!")
               .MinimumLength(6).WithMessage("Min 6 ola biler!");

            RuleFor(portfolio => portfolio.Description)
              .NotEmpty().WithMessage("Bos ola bilmez!")
              .NotNull().WithMessage("Null ola bilmez!")
              .MaximumLength(150).WithMessage("Max 150 ola biler!")
              .MinimumLength(10).WithMessage("Min 10 ola biler!");

            RuleFor(portfolio => portfolio.ProjectUrl)
              .NotEmpty().WithMessage("Bos ola bilmez!")
              .NotNull().WithMessage("Null ola bilmez!")
              .MaximumLength(50).WithMessage("Max 50 ola biler!")
              .MinimumLength(5).WithMessage("Min 5 ola biler!");


            RuleFor(portfolio => portfolio.Client)
              .NotEmpty().WithMessage("Bos ola bilmez!")
              .NotNull().WithMessage("Null ola bilmez!")
              .MaximumLength(30).WithMessage("Max 50 ola biler!")
              .MinimumLength(5).WithMessage("Min 5 ola biler!");


            RuleFor(portfolio => portfolio.ProjectDate)
              .NotEmpty().WithMessage("Bos ola bilmez!")
              .NotNull().WithMessage("Null ola bilmez!");

            RuleFor(portfolio => portfolio.CategoryId)
             .NotEmpty().WithMessage("Bos ola bilmez!")
             .NotNull().WithMessage("Null ola bilmez!");


            RuleFor(portfolio => portfolio.PortfolioItemImage)
               .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!");

            RuleFor(portfolio => portfolio.PortfolioSlideImages)
             .NotEmpty().WithMessage("Bos ola bilmez!")
             .NotNull().WithMessage("Null ola bilmez!");


        }
    }
}
