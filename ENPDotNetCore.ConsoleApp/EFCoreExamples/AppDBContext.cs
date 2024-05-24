using ENPDotNetCore.ConsoleApp.Dtos;
using ENPDotNetCore.ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENPDotNetCore.ConsoleApp.EFCoreExamples;

internal class AppDBContext : DbContext
{
    // write override onCon
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    }
    public DbSet<BlogDto> Blogs { get; set; }
}
