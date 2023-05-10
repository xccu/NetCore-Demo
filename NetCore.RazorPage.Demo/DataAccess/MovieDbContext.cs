using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class MovieDbContext : DbContext
{
    public MovieDbContext (DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movie { get; set; } = default!;
}
