using ApiProject.Business.DTO.ProfessionDtos;
using ApiProject.Core.Entites;
using ApiProject.Core.Repositories;
using ApiProject.Data.Repositories.Implementations;
using AutoMapper;
using AutoMapper.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.Services.Implementations
{

    public class ProfessionService : IProfessionService
    {
        private readonly IProfessionRepository _professionRepository;
        private readonly IMapper _mapper;

        public ProfessionService(IProfessionRepository professionRepository, IMapper mapper)
        {
            _professionRepository = professionRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync([FromForm] ProfessionCreateDto professionCreateDto)
        {
            Profession profession = _mapper.Map<Profession>(professionCreateDto);
            profession.IsDeleted = false;

            await _professionRepository.CreateAsync(profession);
            await _professionRepository.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            Profession profession = await _professionRepository.GetByIdAsync(feature => feature.Id == id);

            if (profession == null) throw new NullReferenceException("profession couldn't be null!");

            _professionRepository.Delete(profession);
            await _professionRepository.SaveChanges();
        }

        public async Task<IEnumerable<ProfessionGetDto>> GetAllAsync()
        {
            List<Profession> professions = await _professionRepository.GetAllAsync(profession => profession.IsDeleted == false);

            if (professions == null) throw new NullReferenceException("professions couldn't be null!");

            IEnumerable<ProfessionGetDto> professionGetDtos = professions.Select(profession => new ProfessionGetDto { Name = profession.Name });

            return professionGetDtos;
        }

        public async Task<ProfessionGetDto> GetByIdAsync(int id)
        {
            Profession profession = await _professionRepository.GetByIdAsync(profession => profession.Id == id && profession.IsDeleted == false);

            if (profession == null) throw new NullReferenceException("profession couldn't be null!");

            ProfessionGetDto professionGetDto = _mapper.Map<ProfessionGetDto>(profession);

            return professionGetDto;
        }

        public async Task ToggleDelete(int id)
        {
            Profession profession = await _professionRepository.GetByIdAsync(profession => profession.Id == id);

            if (profession == null) throw new NullReferenceException("profession couldn't be null!");

            profession.IsDeleted = !profession.IsDeleted;
            profession.DeletedDate = DateTime.UtcNow.AddHours(4);

            await _professionRepository.SaveChanges();
        }

        public async Task UpdateAsync([FromForm] ProfessionUpdateDto professionUpdateDto)
        {
            Profession profession = await _professionRepository.GetByIdAsync(profession => profession.Id == professionUpdateDto.Id);

            if (profession == null) throw new NullReferenceException("profession couldn't be null!");

            profession = _mapper.Map(professionUpdateDto, profession);
            profession.UpdatedDate = DateTime.UtcNow.AddHours(4);

            await _professionRepository.SaveChanges();
        }
    }
}
