using Microsoft.AspNetCore.Mvc;

namespace ApiTest1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        public static List<Car> listOfCars = new List<Car>()
        {
            new Car()
            {
                Id = 1,
                Brand = "Honda",
                Model = "City",
                Year = 2018,
                Color = "Silver Metalic"
            },
            new Car()
            {
                Id = 1,
                Brand = "Toyota",
                Model = "Vios",
                Year = 2015,
                Color = "White"
            },
            new Car()
            {
                Id = 1,
                Brand = "Honda",
                Model = "CRV",
                Year = 2022,
                Color = "Black"
            },
            new Car()
            {
                Id = 1,
                Brand = "Honda",
                Model = "HRV",
                Year = 2025,
                Color = "Black"
            }
        };

        [HttpGet]
        public List<Car> GetAllCars()
        {
            return listOfCars;
        }

        [HttpGet("{id}")]
        public Car GetCar(int id) 
        {
            Car car = listOfCars.Where(c => c.Id == id).FirstOrDefault();

            return car;
        }

        [HttpPost]
        public List<Car> AddCar([FromBody] Car car)
        {
            listOfCars.Add(car);

            return listOfCars;
        }

        [HttpPut("{id}")]
        public Car UpdateCar([FromRoute] int id, [FromBody] Car car)
        {
            Car carToBeUpdated = listOfCars.Where(c => c.Id == id).FirstOrDefault();

            if (carToBeUpdated != null) 
            { 
                carToBeUpdated.Brand = car.Brand;
                carToBeUpdated.Model = car.Model;
                carToBeUpdated.Year = car.Year;
                carToBeUpdated.Color = car.Color;

                return carToBeUpdated;
            }

            return null;
        }

        [HttpDelete("{id}")]
        public bool DeleteCar([FromRoute]int id) 
        {
            Car carToBeDeleted = listOfCars.Where(c => c.Id == id).FirstOrDefault();

            if (carToBeDeleted != null) { 
                listOfCars.Remove(carToBeDeleted);

                return true;
            }

            return false;
        }

    }

    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
    }
}
