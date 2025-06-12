using System;
using System.Collections.Generic;

namespace APBD_MockTest2;

public partial class Car
{
    public int Id { get; set; }

    public string ModelName { get; set; } = null!;

    public int CarManufacturerId { get; set; }

    public int Number { get; set; }

    public virtual CarManufacturer CarManufacturer { get; set; } = null!;

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
}
