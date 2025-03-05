using System;
using System.Collections.Generic;

namespace ApiTest1.Entities;

public partial class Car
{
    public int CarId { get; set; }

    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public string Color { get; set; } = null!;

    public int? CarBrandId { get; set; }

    public virtual CarBrand? CarBrand { get; set; }
}
