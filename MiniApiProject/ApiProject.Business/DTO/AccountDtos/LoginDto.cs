using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.DTO.AccountDtos
{
    public class LoginDto
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(user => user.UsernameOrEmail)
               .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!")
               .MaximumLength(30).WithMessage("Max 30 ola biler!")
               .MinimumLength(5).WithMessage("Min 5 ola biler!");

            RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Bos ola bilmez!")
            .NotNull().WithMessage("Null ola bilmez!")
            .MaximumLength(30).WithMessage("Max 30 ola biler!")
            .MinimumLength(8).WithMessage("Min 8 ola biler!");
        }
    }
}
