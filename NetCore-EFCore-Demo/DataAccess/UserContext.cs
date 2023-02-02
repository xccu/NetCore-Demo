using Microsoft.EntityFrameworkCore;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Course>();
            modelBuilder.Entity<UserCourse>().HasKey(t => new { t.userId, t.courseId });
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<UserCourse> UserCourse { get; set; }
    }
}
