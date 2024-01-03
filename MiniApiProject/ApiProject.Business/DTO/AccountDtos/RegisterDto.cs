using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.DTO.AccountDtos
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(user => user.FullName)
               .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!")
               .MaximumLength(30).WithMessage("Max 30 ola biler!")
               .MinimumLength(5).WithMessage("Min 5 ola biler!");

            RuleFor(user => user.UserName)
             .NotEmpty().WithMessage("Bos ola bilmez!")
             .NotNull().WithMessage("Null ola bilmez!")
             .MaximumLength(30).WithMessage("Max 30 ola biler!")
             .MinimumLength(5).WithMessage("Min 5 ola biler!");

            RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Bos ola bilmez!")
            .NotNull().WithMessage("Null ola bilmez!")
            .MaximumLength(50).WithMessage("Max 50 ola biler!")
            .MinimumLength(10).WithMessage("Min 10 ola biler!");

            RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Bos ola bilmez!")
            .NotNull().WithMessage("Null ola bilmez!")
            .MaximumLength(30).WithMessage("Max 30 ola biler!")
            .MinimumLength(8).WithMessage("Min 8 ola biler!");
        }
    }
}
