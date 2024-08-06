// See https://aka.ms/new-console-template for more information
using ENPDotNetCore.ConsoleAppEFCore.Databases.Models;

Console.WriteLine("Hello, World!");


AppDbContext appDbContext = new AppDbContext();
appDbContext.TblPieCharts.ToList();