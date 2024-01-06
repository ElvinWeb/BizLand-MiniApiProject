using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.DTO.WorkerDtos
{
    public class WorkerUpdateDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string MediaUrl { get; set; }
        public int ProfessionId { get; set; }
        public IFormFile ImgFile { get; set; }
    }

    public class WorkerUpdateDtoValidator : AbstractValidator<WorkerUpdateDto>
    {
        public WorkerUpdateDtoValidator()
        {

            RuleFor(worker => worker.Id)
             .NotEmpty().WithMessage("Bos ola bilmez!")
             .NotNull().WithMessage("Null ola bilmez!")
             .GreaterThanOrEqualTo(1).WithMessage("Id menfi deyer ola bilmez!");

            RuleFor(worker => worker.FullName)
               .NotEmpty().WithMessage("Bos ola bilmez!")
               .NotNull().WithMessage("Null ola bilmez!")
               .MaximumLength(30).WithMessage("Max 30 ola biler!")
               .MinimumLength(6).WithMessage("Min 6 ola biler!");

            RuleFor(worker => worker.MediaUrl)
              .NotEmpty().WithMessage("Bos ola bilmez!")
              .NotNull().WithMessage("Null ola bilmez!")
              .MaximumLength(50).WithMessage("Max 50 ola biler!")
              .MinimumLength(10).WithMessage("Min 10 ola biler!");

            RuleFor(worker => worker.ProfessionId)
                .NotEmpty().WithMessage("Bos ola bilmez!")
                .NotNull().WithMessage("Null ola bilmez!")
                .GreaterThanOrEqualTo(1).WithMessage("Id menfi deyer ola bilmez!");

            RuleFor(worker => worker.ImgFile)
              .NotEmpty().WithMessage("Bos ola bilmez!")
              .NotNull().WithMessage("Null ola bilmez!");

        }
    }
}
