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
    public interface IFeatureService
    {
        Task CreateAsync([FromForm] FeatureCreateDto featureCreateDto);
        Task UpdateAsync([FromForm] FeatureUpdateDto featureUpdateDto);
        Task DeleteAsync(int id);
        Task ToggleDelete(int id);
        Task<FeatureGetDto> GetByIdAsync(int id);
        Task<IEnumerable<FeatureGetDto>> GetAllAsync();
    }
}
