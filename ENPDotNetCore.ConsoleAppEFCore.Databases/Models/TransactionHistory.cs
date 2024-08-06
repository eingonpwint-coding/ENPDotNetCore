using System;
using System.Collections.Generic;

namespace ENPDotNetCore.ConsoleAppEFCore.Databases.Models;

public partial class TransactionHistory
{
    public long TransactionHistoryId { get; set; }

    public string FromAccountNo { get; set; } = null!;

    public string ToAccountNo { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }
}
