using Microsoft.EntityFrameworkCore;

namespace WebAPI;

public class BarDbContext : DbContext
{
    public DbSet<Bar> Bars { get; set; }

    protected BarDbContext() : base() { }

    public BarDbContext(DbContextOptions<BarDbContext> options) : base(options)
    {

    }
}
