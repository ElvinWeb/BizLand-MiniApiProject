using ApiProject.Business.DTO.CategoryDtos;
using ApiProject.Core.Entites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryService _categoryService;

        public CategoryService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public Task CreateAsync([FromForm] CategoryCreateDto categoryCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryGetDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryGetDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Task ToggleDelete(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync([FromForm] CategoryUpdateDto categoryUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
