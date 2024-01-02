using ApiProject.Business.DTO.ServiceDtos;
using ApiProject.Core.Entites;
using ApiProject.Core.Repositories;
using ApiProject.Data.Repositories.Implementations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.Services.Implementations
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;
        private readonly IMapper _mapper;

        public FeatureService(IFeatureRepository featureRepository, IMapper mapper)
        {
            _featureRepository = featureRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync([FromForm] FeatureCreateDto featureCreateDto)
        {
            Feature feature = _mapper.Map<Feature>(featureCreateDto);
            feature.IsDeleted = false;

            await _featureRepository.CreateAsync(feature);
            await _featureRepository.SaveChanges();

        }

        public async Task DeleteAsync(int id)
        {
            Feature feature = await _featureRepository.GetByIdAsync(feature => feature.Id == id);

            if (feature == null) throw new NullReferenceException("feature couldn't be null!");

            _featureRepository.Delete(feature);
            await _featureRepository.SaveChanges();
        }

        public async Task<IEnumerable<FeatureGetDto>> GetAllAsync()
        {
            IEnumerable<Feature> features = await _featureRepository.GetAllAsync(feature => feature.IsDeleted == false);

            if (features == null) throw new NullReferenceException("features couldn't be null!");

            IEnumerable<FeatureGetDto> featureGetDtos = features.Select(feature => new FeatureGetDto { Description = feature.Description, Title = feature.Title });

            return featureGetDtos;
        }

        public async Task<FeatureGetDto> GetByIdAsync(int id)
        {
            Feature feature = await _featureRepository.GetByIdAsync(feature => feature.Id == id);

            if (feature == null) throw new NullReferenceException("feature couldn't be null!");

            FeatureGetDto featureGetDto = _mapper.Map<FeatureGetDto>(feature);

            return featureGetDto;
        }


        public async Task ToggleDelete(int id)
        {
            Feature feature = await _featureRepository.GetByIdAsync(feature => feature.Id == id);

            if (feature == null) throw new NullReferenceException("feature couldn't be null!");

            feature.IsDeleted = !feature.IsDeleted;
            feature.DeletedDate = DateTime.UtcNow.AddHours(4);

            await _featureRepository.SaveChanges();

        }

        public async Task UpdateAsync([FromForm] FeatureUpdateDto featureUpdateDto)
        {
            Feature feature = await _featureRepository.GetByIdAsync(feature => feature.Id == featureUpdateDto.Id);

            if (feature == null) throw new NullReferenceException("feature couldn't be null!");


            feature = _mapper.Map(featureUpdateDto, feature);
            await _featureRepository.SaveChanges();
        }
    }
}
