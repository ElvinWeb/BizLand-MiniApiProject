using ApiProject.Business.DTO.AccountDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.DTO.CategoryDtos
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
    }

    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(category => category.Name)
               .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!")
               .MaximumLength(20).WithMessage("Max 20 ola biler!")
               .MinimumLength(3).WithMessage("Min 3 ola biler!");
        }
    }
}
