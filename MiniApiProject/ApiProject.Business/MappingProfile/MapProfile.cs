using ApiProject.Business.DTO.CategoryDtos;
using ApiProject.Business.DTO.ProfessionDtos;
using ApiProject.Business.DTO.ServiceDtos;
using ApiProject.Business.DTO.WorkerDtos;
using ApiProject.Core.Entites;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.MappingProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<WorkerCreateDto, Worker>().ReverseMap();
            CreateMap<WorkerGetDto, Worker>().ReverseMap();
            CreateMap<WorkerUpdateDto, Worker>().ReverseMap();

            CreateMap<FeatureCreateDto, Feature>().ReverseMap();
            CreateMap<FeatureGetDto, Feature>().ReverseMap();
            CreateMap<FeatureUpdateDto, Feature>().ReverseMap();

            CreateMap<ProfessionCreateDto, Profession>().ReverseMap();
            CreateMap<ProfessionGetDto, Profession>().ReverseMap();
            CreateMap<ProfessionUpdateDto, Profession>().ReverseMap();

            CreateMap<CategoryCreateDto, Category>().ReverseMap();
            CreateMap<CategoryGetDto, Category>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
        }
    }
}
