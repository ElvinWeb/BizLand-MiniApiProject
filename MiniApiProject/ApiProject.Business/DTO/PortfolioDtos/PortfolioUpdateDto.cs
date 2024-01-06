using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.DTO.PortfolioDtos
{
    public class PortfolioUpdateDto
    {
        public int Id { get; set; }
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


    public class PortfolioUpdateDtoValidator : AbstractValidator<PortfolioUpdateDto>
    {
        public PortfolioUpdateDtoValidator()
        {

            RuleFor(portfolio => portfolio.Id)
              .NotEmpty().WithMessage("Bos ola bilmez!")
              .NotNull().WithMessage("Null ola bilmez!")
              .GreaterThanOrEqualTo(1).WithMessage("Id menfi bir deyer ola bilmez!");

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
             .NotNull().WithMessage("Null ola bilmez!")
             .GreaterThanOrEqualTo(1).WithMessage("Id menfi bir deyer ola bilmez!");


            RuleFor(portfolio => portfolio.PortfolioItemImage)
               .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!");

            RuleFor(portfolio => portfolio.PortfolioSlideImages)
             .NotEmpty().WithMessage("Bos ola bilmez!")
             .NotNull().WithMessage("Null ola bilmez!");


        }
    }
}
