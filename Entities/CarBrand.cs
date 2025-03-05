using System;
using System.Collections.Generic;

namespace ApiTest1.Entities;

public partial class CarBrand
{
    public int CarBrandId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
