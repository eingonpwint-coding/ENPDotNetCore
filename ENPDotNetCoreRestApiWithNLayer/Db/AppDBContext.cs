namespace ENPDotNetCore.RestApiWithNLayer.Db
{
    internal class AppDBContext : DbContext
    {
        // write override onCon
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<BlogModel> Blogs { get; set; }

    }
}
