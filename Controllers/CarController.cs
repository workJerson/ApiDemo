using ApiTest1.Context;
using ApiTest1.Contracts;
using ApiTest1.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTest1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService carService;

        public CarController(ICarService carService) 
        { 
            this.carService = carService;
        }

        [HttpGet]
        public async Task<List<Car>> GetAllCars()
        {
            return await carService.GetAllCars();
        }

        [HttpGet("{id}")]
        public async Task<Car> GetCarById([FromRoute] int id)
        {
            return await carService.GetCarById(id);
        }

        [HttpPost]
        public async Task<Car> CreateCar([FromBody] Car car)
        {
            return await carService.CreateCar(car);
        }

        [HttpPut("{id}")] 
        public async Task<Car?> UpdateCar([FromRoute] int id, [FromBody] Car car)
        {
            return await carService.UpdateCar(car, id);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteCar([FromRoute] int id)
        {
            return await carService.DeleteCar(id);
        }
    }
}
