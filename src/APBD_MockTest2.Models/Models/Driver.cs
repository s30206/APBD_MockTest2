using System;
using System.Collections.Generic;

namespace APBD_MockTest2;

public partial class Driver
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public int CarId { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual ICollection<DriverCompetition> DriverCompetitions { get; set; } = new List<DriverCompetition>();
}
