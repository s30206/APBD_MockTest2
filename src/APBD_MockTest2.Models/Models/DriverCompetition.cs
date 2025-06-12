using System;
using System.Collections.Generic;

namespace APBD_MockTest2;

public partial class DriverCompetition
{
    public int DriverId { get; set; }

    public int CompetitionId { get; set; }

    public DateTime Date { get; set; }

    public virtual Competition Competition { get; set; } = null!;

    public virtual Driver Driver { get; set; } = null!;
}
