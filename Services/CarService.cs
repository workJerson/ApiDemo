using ApiTest1.Context;
using ApiTest1.Entities;
using ApiTest1.Repostories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ApiTest1.Contracts
{
    public interface ICarService
    {
        Task<List<Car>> GetAllCars();
        Task<Car> GetCarById(int id);
        Task<Car> CreateCar(Car car);
        Task<Car?> UpdateCar(Car car, int id);
        Task<bool> DeleteCar(int id);
    }
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;

        public DatabaseContext databaseContext;
        public CarService(DatabaseContext databaseContext, ICarRepository carRepository)
        {
            this.databaseContext = databaseContext;
            this.carRepository = carRepository;
        }

        public async Task<Car> CreateCar(Car car)
        {
            return await carRepository.CreateCar(car);
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

        public async Task<List<Car>> GetAllCars()
        {
            List<Car> listOfCars = await databaseContext.Cars.ToListAsync();

            return listOfCars;
        }

        public async Task<Car> GetCarById(int id)
        {
            Car? car = await databaseContext.Cars.Where(car => car.CarId == id).FirstOrDefaultAsync();

            if (car != null)
                return car;

            return null;
        }

        public async Task<Car?> UpdateCar(Car car, int id)
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
            return carFromDatabase;
        }
    }
}
