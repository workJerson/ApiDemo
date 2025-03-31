using ApiTest1.Context;
using ApiTest1.Contracts;
using ApiTest1.Dtos;
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
        public async Task<IActionResult> GetAllCars(
            [FromQuery] string? quickSearch = null
            , [FromQuery] string? model = null
            , [FromQuery] int? year = null
            , [FromQuery] string? color = null
            , [FromQuery] string? brandName = null
            , [FromQuery] bool isDescending = true
            , [FromQuery] string? orderBy = null
            )
        {
            var listOfCars = await carService.GetAllCars(quickSearch, model, year, color, brandName, isDescending, orderBy);

            return Ok(listOfCars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById([FromRoute] int id)
        {
            var car =  await carService.GetCarById(id);

            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] CreateCarModel car)
        {
            var createCarResult =  await carService.CreateCar(car);

            return Ok(createCarResult);
        }

        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateCar([FromRoute] int id, [FromBody] UpdateCarModel car)
        {
            var updateCarResult =  await carService.UpdateCar(car, id);

            return Ok(updateCarResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar([FromRoute] int id)
        {
            var deleteCarResult =  await carService.DeleteCar(id);

            if (deleteCarResult)
                return Ok(deleteCarResult);
            else
                return BadRequest(deleteCarResult);
        }
    }
}
