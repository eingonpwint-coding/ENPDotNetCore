using Microsoft.EntityFrameworkCore;
using ENPDotNetCore.NLayer.DataAccess.Models;

namespace ENPDotNetCore.NLayer.DataAccess.Db;

internal class AppDBContext : DbContext
{
    // write override onCon
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    }
    public DbSet<BlogModel> Blogs { get; set; }
}
