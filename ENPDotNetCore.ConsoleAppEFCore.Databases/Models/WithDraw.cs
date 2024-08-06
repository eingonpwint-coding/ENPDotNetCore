﻿using System;
using System.Collections.Generic;

namespace ENPDotNetCore.ConsoleAppEFCore.Databases.Models;

public partial class WithDraw
{
    public long WithDrawId { get; set; }

    public string AccountNo { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime WithDrawDate { get; set; }
}
