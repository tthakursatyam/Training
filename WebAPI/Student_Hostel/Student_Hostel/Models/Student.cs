using System;
using System.Collections.Generic;

namespace Student_Hostel.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public string? Gender { get; set; }

    public int? Age { get; set; }

    public string? Course { get; set; }

    public virtual Hostel? Hostel { get; set; }
}
