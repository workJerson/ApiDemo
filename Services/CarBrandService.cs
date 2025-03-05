using ApiTest1.Context;
using ApiTest1.Contracts;
using ApiTest1.Entities;

namespace ApiTest1.Services
{
    public interface ICarBrandService
    {
        Task<CarBrand> CreateCarBrand(CarBrand entity);
    }
    public class CarBrandService : ICarBrandService
    {
        private readonly DatabaseContext databaseContext;

        public CarBrandService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<CarBrand> CreateCarBrand(CarBrand entity)
        {
            // Assuming created na yung car brand

            // Create car record

            throw new NotImplementedException();
        }
    }
}
