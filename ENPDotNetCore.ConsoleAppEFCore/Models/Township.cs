﻿using System;
using System.Collections.Generic;

namespace ENPDotNetCore.ConsoleAppEFCore.Models;

public partial class Township
{
    public long TownshipId { get; set; }

    public string TownshipCode { get; set; } = null!;

    public string TownshipName { get; set; } = null!;

    public string StateCode { get; set; } = null!;
}
