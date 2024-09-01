using ENPDotNetCore.MvcAppWithAjax.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ENPDotNetCore.MvcAppWithAjax.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<BlogEntity> Blogs { get; set; }
}
//DataModel

//public class BlogEntity
//{
   

//}

//ViewModel
//public class BlogModel
//{

//}
