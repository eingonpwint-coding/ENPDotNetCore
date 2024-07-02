using Microsoft.EntityFrameworkCore;
using ENPDotNetCore.MvcApp2.Models;
namespace ENPDotNetCore.MvcApp2.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogModel> Blogs { get; set; }
    }
}
