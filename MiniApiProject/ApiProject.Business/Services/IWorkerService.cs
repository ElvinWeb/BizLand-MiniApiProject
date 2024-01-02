using ApiProject.Business.DTO.ProfessionDtos;
using ApiProject.Business.DTO.WorkerDtos;
using ApiProject.Core.Entites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.Services
{
    public interface IWorkerService
    {
        Task CreateAsync([FromForm] WorkerCreateDTO workerCreateDTO);
        Task UpdateAsync([FromForm] WorkerUpdateDto workerUpdateDto);
        Task DeleteAsync(int id);
        Task ToggleDelete(int id);
        Task<WorkerGetDto> GetByIdAsync(int id);
        Task<IEnumerable<WorkerGetDto>> GetAllAsync();
    }
}
