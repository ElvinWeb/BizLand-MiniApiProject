using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.DTO.ProfessionDtos
{
    public class ProfessionUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProfessionUpdateDtoValidator : AbstractValidator<ProfessionUpdateDto>
    {
        public ProfessionUpdateDtoValidator()
        {
            RuleFor(profession => profession.Name)
               .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!")
               .MaximumLength(30).WithMessage("Max 30 ola biler!")
               .MinimumLength(3).WithMessage("Min 3 ola biler!");
        }
    }
}
