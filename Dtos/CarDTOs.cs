namespace ApiTest1.Dtos
{
    public class CreateCarModel
    {
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public string Color { get; set; } = null!;
        public int? CarBrandId { get; set; }
    }

    public class UpdateCarModel : CreateCarModel
    {
        public int CarId { get; set; }
    }

    public class GetCarModel : UpdateCarModel
    {
        public GetCarBrandDto? CarBrand { get; set; }
    }
}
