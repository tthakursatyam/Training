using System;
using System.Collections.Generic;

namespace Student_Hostel.Models;

public partial class Hostel
{
    public int HostelId { get; set; }

    public int RoomNumber { get; set; }

    public int? StudentId { get; set; }

    public virtual Student? Student { get; set; }
}
