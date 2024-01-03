using ApiProject.Business.DTO.CategoryDtos;
using ApiProject.Business.DTO.ServiceDtos;
using ApiProject.Core.Entites;
using ApiProject.Core.Repositories;
using AutoMapper;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync([FromForm] CategoryCreateDto categoryCreateDto)
        {
            Category category = _mapper.Map<Category>(categoryCreateDto);
            category.IsDeleted = false;

            await _categoryRepository.CreateAsync(category);
            await _categoryRepository.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(category => category.Id == id);

            if (category == null) throw new NullReferenceException("category couldn't be null!");

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChanges();
        }

        public async Task<IEnumerable<CategoryGetDto>> GetAllAsync()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllAsync(category => category.IsDeleted == false);

            if (categories == null) throw new NullReferenceException("categories couldn't be null!");

            IEnumerable<CategoryGetDto> categoryGetDtos = categories.Select(category => new CategoryGetDto { Id = category.Id, Name = category.Name });

            return categoryGetDtos;
        }

        public async Task<CategoryGetDto> GetByIdAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(category => category.Id == id);

            if (category == null) throw new NullReferenceException("category couldn't be null!");

            CategoryGetDto categoryGetDto = _mapper.Map<CategoryGetDto>(category);

            return categoryGetDto;
        }


        public async Task ToggleDelete(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(category => category.Id == id);

            if (category == null) throw new NullReferenceException("category couldn't be null!");

            category.IsDeleted = !category.IsDeleted;
            category.DeletedDate = DateTime.UtcNow.AddHours(4);

            await _categoryRepository.SaveChanges();
        }

        public async Task UpdateAsync([FromForm] CategoryUpdateDto categoryUpdateDto)
        {
            Category category = await _categoryRepository.GetByIdAsync(category => category.Id == categoryUpdateDto.Id);

            if (category == null) throw new NullReferenceException("feature couldn't be null!");


            category = _mapper.Map(categoryUpdateDto, category);
            await _categoryRepository.SaveChanges();
        }
    }
}
