using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ENPDotNetCore.ConsoleAppEFCore.Databases.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Deposit> Deposits { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    public virtual DbSet<TblBlogTest> TblBlogTests { get; set; }

    public virtual DbSet<TblBlogTestingChange> TblBlogTestingChanges { get; set; }

    public virtual DbSet<TblEmployee> TblEmployees { get; set; }

    public virtual DbSet<TblExpense> TblExpenses { get; set; }

    public virtual DbSet<TblLogEvent> TblLogEvents { get; set; }

    public virtual DbSet<TblPieChart> TblPieCharts { get; set; }

    public virtual DbSet<TblPizza> TblPizzas { get; set; }

    public virtual DbSet<TblPizzaExtra> TblPizzaExtras { get; set; }

    public virtual DbSet<TblPizzaOrder> TblPizzaOrders { get; set; }

    public virtual DbSet<TblPizzaOrderDetail> TblPizzaOrderDetails { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<Township> Townships { get; set; }

    public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }

    public virtual DbSet<WithDraw> WithDraws { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=ENPDotNetCore;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.AccountLevel).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.AccountNo).HasMaxLength(50);
            entity.Property(e => e.Balance).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.CustomerCode).HasMaxLength(50);
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.StateCode).HasMaxLength(50);
            entity.Property(e => e.TownshipCode).HasMaxLength(50);
        });

        modelBuilder.Entity<Deposit>(entity =>
        {
            entity.ToTable("Deposit");

            entity.Property(e => e.AccountNo).HasMaxLength(50);
            entity.Property(e => e.Amount).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.DepositDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("State");

            entity.Property(e => e.StateId).ValueGeneratedNever();
            entity.Property(e => e.StateCode).HasMaxLength(50);
            entity.Property(e => e.StateName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_Blog");

            entity.Property(e => e.BlogAuthor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BlogContent)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BlogTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblBlogTest>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_BlogTest");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            entity.Property(e => e.BlogCategory).HasMaxLength(50);
            entity.Property(e => e.BlogContent).HasMaxLength(50);
            entity.Property(e => e.BlogStatus).HasMaxLength(50);
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        modelBuilder.Entity<TblBlogTestingChange>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_BlogTestingChange");

            entity.Property(e => e.BlogAuthor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BlogCategory)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BlogContent)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BlogStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BlogTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblEmployee>(entity =>
        {
            entity.ToTable("Tbl_Employee");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TblExpense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId);

            entity.ToTable("Tbl_Expense");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<TblLogEvent>(entity =>
        {
            entity.ToTable("Tbl_LogEvents");

            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblPieChart>(entity =>
        {
            entity.HasKey(e => e.PieChartId);

            entity.ToTable("Tbl_PieChart");

            entity.Property(e => e.PieChartName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PieChartValue).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TblPizza>(entity =>
        {
            entity.HasKey(e => e.PizzaId);

            entity.ToTable("Tbl_Pizza");

            entity.Property(e => e.Pizza)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TblPizzaExtra>(entity =>
        {
            entity.HasKey(e => e.PizzaExtraId);

            entity.ToTable("Tbl_PizzaExtra");

            entity.Property(e => e.PizzaExtraName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TblPizzaOrder>(entity =>
        {
            entity.HasKey(e => e.PizzaOrderId);

            entity.ToTable("Tbl_PizzaOrder");

            entity.Property(e => e.PizzaOrderInvoiceNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TblPizzaOrderDetail>(entity =>
        {
            entity.HasKey(e => e.PizzaOrderDetailId);

            entity.ToTable("Tbl_PizzaOrderDetail");

            entity.Property(e => e.PizzaOrderInvoiceNo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.ToTable("Tbl_User");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<Township>(entity =>
        {
            entity.ToTable("Township");

            entity.Property(e => e.TownshipId).ValueGeneratedNever();
            entity.Property(e => e.StateCode).HasMaxLength(50);
            entity.Property(e => e.TownshipCode).HasMaxLength(50);
            entity.Property(e => e.TownshipName).HasMaxLength(50);
        });

        modelBuilder.Entity<TransactionHistory>(entity =>
        {
            entity.ToTable("TransactionHistory");

            entity.Property(e => e.Amount).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.FromAccountNo).HasMaxLength(50);
            entity.Property(e => e.ToAccountNo).HasMaxLength(50);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<WithDraw>(entity =>
        {
            entity.ToTable("WithDraw");

            entity.Property(e => e.AccountNo).HasMaxLength(50);
            entity.Property(e => e.Amount).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.WithDrawDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
