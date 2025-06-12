﻿using System;
using System.Collections.Generic;

namespace APBD_MockTest2;

public partial class CarManufacturer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
