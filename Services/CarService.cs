using ApiTest1.Context;
using ApiTest1.Dtos;
using ApiTest1.Entities;
using ApiTest1.Repostories;
using ApiTest1.Validators;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTest1.Contracts
{
    public interface ICarService
    {
        Task<List<GetCarModel>> GetAllCars(string? quickSearch = null, string? model = null, int? year = null, string? color = null, string? brandName = null, bool isDescending = true, string? orderBy = null);
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

        public async Task<List<GetCarModel>> GetAllCars(string? quickSearch = null, string? model = null, int? year = null, string? color = null, string? brandName = null, bool isDescending = true, string? orderBy = null)
        {
            var query = databaseContext.Cars
                .AsNoTracking()
                .Include(c => c.CarBrand)
                .AsQueryable();

            // QuickSearch
            if (quickSearch != null)
            {
                query = query.Where(
                    c =>
                        c.Model.Contains(quickSearch) 
                        || c.Color.Contains(quickSearch)
                        || c.CarBrand.Name.Contains(quickSearch) 
                    );
            }

            // Advanced Filter
            if (model != null)
                query = query.Where(c => c.Model == model);

            if (year != null)
                query = query.Where(c => c.Year == year);

            if (color != null)
                query = query.Where(c => c.Color == color);

            if (brandName != null)
                query = query.Where(a => a.CarBrand.Name == brandName);

            // Sorting
            if (orderBy != null)
            {
                if (orderBy == "carId")
                {
                    if (isDescending)
                        query = query.OrderByDescending(c => c.CarId);
                    else
                        query = query.OrderBy(c => c.CarId);
                }

                if (orderBy == "model")
                {
                    if (isDescending)
                        query = query.OrderByDescending(c => c.Model);
                    else
                        query = query.OrderBy(c => c.Model);
                }
            }
               
            List<Car> listOfCars = await query.ToListAsync();

            return mapper.Map<List<GetCarModel>>(listOfCars);
        }

        public async Task<GetCarModel> GetCarById(int id)
        {
            Car? car = await databaseContext.Cars
                .AsSplitQuery()
                .Include(c => c.CarBrand)
                .Where(car => car.CarId == id)
                .FirstOrDefaultAsync();

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
