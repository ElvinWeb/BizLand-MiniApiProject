using ApiProject.Business.DTO.CategoryDtos;
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
    public interface ICategoryService
    {
        Task CreateAsync([FromForm] CategoryCreateDto categoryCreateDto);
        Task UpdateAsync([FromForm] CategoryUpdateDto categoryUpdateDto);
        Task DeleteAsync(int id);
        Task ToggleDelete(int id);
        Task<CategoryGetDto> GetByIdAsync(int id);
        Task<IEnumerable<CategoryGetDto>> GetAllAsync();
    }
}
