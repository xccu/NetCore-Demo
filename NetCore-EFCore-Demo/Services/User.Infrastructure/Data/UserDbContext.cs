using Microsoft.EntityFrameworkCore;
using User.ApplicationCore.Entities;


namespace User.Infrastructure.Data;

public partial class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options): base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<ApplicationCore.Entities.User>();
        //modelBuilder.Entity<Course>();
        //modelBuilder.Entity<UserCourse>().HasKey(t => new { t.userId, t.courseId });
    }

    public virtual DbSet<ApplicationCore.Entities.User> User { get; set; }
    public virtual DbSet<Course> Course { get; set; }
    public virtual DbSet<UserCourse> UserCourse { get; set; }
}
