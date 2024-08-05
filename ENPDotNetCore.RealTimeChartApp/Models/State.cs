using System;
using System.Collections.Generic;

namespace ENPDotNetCore.RealTimeChartApp.Models;

public partial class State
{
    public long StateId { get; set; }

    public string StateCode { get; set; } = null!;

    public string StateName { get; set; } = null!;
}
