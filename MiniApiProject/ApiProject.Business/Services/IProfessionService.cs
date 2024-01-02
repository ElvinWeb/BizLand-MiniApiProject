using ApiProject.Business.DTO.ProfessionDtos;
using ApiProject.Business.DTO.ServiceDtos;
using ApiProject.Core.Entites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.Services
{
    public interface IProfessionService
    {
        Task CreateAsync([FromForm] ProfessionCreateDto professionCreateDto);
        Task UpdateAsync([FromForm] ProfessionUpdateDto professionUpdateDto);
        Task DeleteAsync(int id);
        Task ToggleDelete(int id);
        Task<ProfessionGetDto> GetByIdAsync(int id);
        Task<IEnumerable<ProfessionGetDto>> GetAllAsync();
    }
}
