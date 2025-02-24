using ApiTest1.Context;
using ApiTest1.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTest1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        public DatabaseContext databaseContext;
        public CarController(DatabaseContext databaseContext) 
        { 
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<List<Car>> GetAllCars()
        {
            List<Car> listOfCars = await databaseContext.Cars.ToListAsync();

            return listOfCars;
        }

        [HttpGet("{id}")]
        public async Task<Car> GetCarById([FromRoute] int id)
        {
            Car? car = await databaseContext.Cars.Where(car => car.CarId == id).FirstOrDefaultAsync();

            if (car != null)
                return car;

            return null;
        }

        [HttpPost]
        public async Task<Car> CreateCar([FromBody] Car car)
        {
            await databaseContext.Cars.AddAsync(car);

            await databaseContext.SaveChangesAsync();

            return car;
        }

        [HttpPut("{id}")] 
        public async Task<Car?> UpdateCar([FromRoute] int id, [FromBody] Car car)
        {
            // Check if record exists in database
            var carFromDatabase = await databaseContext.Cars.Where(car => car.CarId == id).FirstOrDefaultAsync();

            if(carFromDatabase == null)
                return null;

            // Actual Update
            carFromDatabase.Brand = car.Brand;
            carFromDatabase.Model = car.Model;
            carFromDatabase.Year = car.Year;
            carFromDatabase.Color = car.Color;

            await databaseContext.SaveChangesAsync();

            // Return result
            return carFromDatabase;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteCar([FromRoute] int id)
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
    }
}
