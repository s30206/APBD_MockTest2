using System;
using System.Collections.Generic;

namespace APBD_MockTest2;

public partial class Competition
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DriverCompetition> DriverCompetitions { get; set; } = new List<DriverCompetition>();
}
