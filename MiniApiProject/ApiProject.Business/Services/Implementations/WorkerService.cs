using ApiProject.Business.DTO.WorkerDtos;
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
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IMapper _mapper;

        public WorkerService(IWorkerRepository workerRepository, IMapper mapper)
        {
            _workerRepository = workerRepository;
            _mapper = mapper;
        }
        public Task CreateAsync([FromForm] WorkerCreateDTO workerCreateDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WorkerGetDto>> GetAllAsync()
        {
            IEnumerable<Worker> workers = await _workerRepository.GetAllAsync(worker => worker.IsDeleted == false, "Profession");

            IEnumerable<WorkerGetDto> workerGetDtos = workers.Select(worker => new WorkerGetDto { FullName = worker.FullName, ImgUrl = worker.ImgUrl, Profession = worker.Profession.Name, MediaUrl = worker.MediaUrl });
        }

        public async Task<WorkerGetDto> GetByIdAsync(int id)
        {
            Worker worker = await _workerRepository.GetByIdAsync(worker => worker.Id == id && worker.IsDeleted == false);

            if (worker == null) throw new NullReferenceException("worker couldn't be null!");

            WorkerGetDto workerGetDto = _mapper.Map<WorkerGetDto>(worker);

            return workerGetDto;
        }

        public Task ToggleDelete(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync([FromForm] WorkerUpdateDto workerUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
