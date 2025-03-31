using ApiTest1.Dtos;
using ApiTest1.Entities;
using AutoMapper;

namespace ApiTest1.AutoMapperProfiles
{
    public class CarBrandMapperProfile : Profile
    {
        public CarBrandMapperProfile() 
        {
            CreateMap<CarBrand, GetCarBrandDto>();
        }
    }
}
