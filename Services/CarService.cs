using ApiTest1.Context;
using ApiTest1.Dtos;
using ApiTest1.Entities;
using ApiTest1.Repostories;
using ApiTest1.Validators;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ApiTest1.Contracts
{
    public interface ICarService
    {
        Task<List<GetCarModel>> GetAllCars();
        Task<GetCarModel> GetCarById(int id);
        Task<GetCarModel> CreateCar(CreateCarModel car);
        Task<GetCarModel?> UpdateCar(UpdateCarModel car, int id);
        Task<bool> DeleteCar(int id);
    }
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;

        public DatabaseContext databaseContext;

        private readonly IMapper mapper;

        public CarService(DatabaseContext databaseContext, ICarRepository carRepository, IMapper mapper)
        {
            this.databaseContext = databaseContext;
            this.carRepository = carRepository;
            this.mapper = mapper;
        }

        public async Task<GetCarModel> CreateCar(CreateCarModel car)
        {
            CreateCarValidator validator = new CreateCarValidator(databaseContext);
            ValidationResult results = validator.Validate(car);

            if (results.Errors.Count > 0)
            {
                throw new Exception(string.Join(",", results.Errors.Select(x => x.ErrorMessage).ToList()));
            }

            var createCarResult =  await carRepository.CreateCar(mapper.Map<Car>(car));
            
            return mapper.Map<GetCarModel>(createCarResult);
        }

        public async Task<bool> DeleteCar(int id)
        {
            // Check if record exists in database
            var carFromDatabase = await databaseContext.Cars.Where(car => car.CarId == id).FirstOrDefaultAsync();

            if (carFromDatabase == null)
                return false;

            // Actual record Deletion
            databaseContext.Remove(carFromDatabase);

            await databaseContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<GetCarModel>> GetAllCars()
        {
            List<Car> listOfCars = await databaseContext.Cars.ToListAsync();

            return mapper.Map<List<GetCarModel>>(listOfCars);
        }

        public async Task<GetCarModel> GetCarById(int id)
        {
            Car? car = await databaseContext.Cars.Where(car => car.CarId == id).FirstOrDefaultAsync();

            if (car != null)
                return mapper.Map<GetCarModel>(car);

            return null;
        }

        public async Task<GetCarModel?> UpdateCar(UpdateCarModel car, int id)
        {
            // Check if record exists in database
            var carFromDatabase = await databaseContext.Cars.Where(car => car.CarId == id).FirstOrDefaultAsync();

            if (carFromDatabase == null)
                return null;

            // Actual Update
            carFromDatabase.Model = car.Model;
            carFromDatabase.Year = car.Year;
            carFromDatabase.Color = car.Color;

            await databaseContext.SaveChangesAsync();

            // Return result
            return mapper.Map<GetCarModel>(carFromDatabase);
        }
    }
}
