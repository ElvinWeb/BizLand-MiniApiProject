using ApiProject.Business.DTO.ServiceDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.DTO.ProfessionDtos
{
    public class ProfessionCreateDto
    {
        public string Name { get; set; }
    }
    public class ProfessionCreateDtoValidator : AbstractValidator<ProfessionCreateDto>
    {
        public ProfessionCreateDtoValidator()
        {
            RuleFor(profession => profession.Name)
               .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!")
               .MaximumLength(30).WithMessage("Max 30 ola biler!")
               .MinimumLength(3).WithMessage("Min 3 ola biler!");
        }
    }
}
