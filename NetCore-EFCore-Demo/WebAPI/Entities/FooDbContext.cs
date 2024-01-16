using Microsoft.EntityFrameworkCore;

namespace WebAPI;

public class FooDbContext : DbContext
{
    public DbSet<Foo> Foos { get; set; }

    protected FooDbContext() : base() { }

    public FooDbContext(DbContextOptions<FooDbContext> options) : base(options)
    {

    }

}
