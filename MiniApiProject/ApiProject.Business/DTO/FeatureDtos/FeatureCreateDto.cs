using ApiProject.Business.DTO.CategoryDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.DTO.ServiceDtos
{
    public class FeatureCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class FeatureCreateDtoValidator : AbstractValidator<FeatureCreateDto>
    {
        public FeatureCreateDtoValidator()
        {
            RuleFor(feature => feature.Title)
               .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!")
               .MaximumLength(20).WithMessage("Max 20 ola biler!")
               .MinimumLength(6).WithMessage("Min 6 ola biler!");

            RuleFor(feature => feature.Description)
              .NotEmpty().WithMessage("Bos ola bilmez!")
              .NotNull().WithMessage("Null ola bilmez!")
              .MaximumLength(150).WithMessage("Max 150 ola biler!")
              .MinimumLength(10).WithMessage("Min 10 ola biler!");

            RuleFor(feature => feature.Icon)
              .NotEmpty().WithMessage("Bos ola bilmez!")
              .NotNull().WithMessage("Null ola bilmez!")
              .MaximumLength(30).WithMessage("Max 30 ola biler!")
              .MinimumLength(10).WithMessage("Min 10 ola biler!");

        }
    }
}
