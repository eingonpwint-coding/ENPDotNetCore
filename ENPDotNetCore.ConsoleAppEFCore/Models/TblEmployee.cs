using System;
using System.Collections.Generic;

namespace ENPDotNetCore.ConsoleAppEFCore.Models;

public partial class TblEmployee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double HourlyRate { get; set; }

    public double HoursWorked { get; set; }
}
