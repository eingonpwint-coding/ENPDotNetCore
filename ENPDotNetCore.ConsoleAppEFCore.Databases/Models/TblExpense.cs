using System;
using System.Collections.Generic;

namespace ENPDotNetCore.ConsoleAppEFCore.Databases.Models;

public partial class TblExpense
{
    public int ExpenseId { get; set; }

    public string Description { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }
}
