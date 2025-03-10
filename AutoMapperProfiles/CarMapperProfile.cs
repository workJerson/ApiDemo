using ApiTest1.Dtos;
using ApiTest1.Entities;
using AutoMapper;

namespace ApiTest1.AutoMapperProfiles
{
    public class CarMapperProfile : Profile
    {
        public CarMapperProfile() 
        {
            CreateMap<CreateCarModel, Car>();
            CreateMap<UpdateCarModel, Car>();
            CreateMap<Car, GetCarModel>();
        }
    }
}
