using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; } = default!;
}
