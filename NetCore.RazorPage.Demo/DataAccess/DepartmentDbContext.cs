using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DepartmentDbContext : DbContext
{
    public DepartmentDbContext(DbContextOptions<DepartmentDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Movie>().ToTable("T_MOVIE");
        //modelBuilder.Entity<Movie>().Property<byte[]>("ConcurrencyToken")
        //    .IsRowVersion();

        //modelBuilder.Entity<Department>().Property<byte[]>("ConcurrencyToken")
        //    .IsConcurrencyToken()
        //    .ValueGeneratedOnAddOrUpdate()
        //    .HasColumnType("rowversion");
    }

    public DbSet<Department> Department { get; set; } = default!;
}