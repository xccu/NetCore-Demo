using Microsoft.EntityFrameworkCore;

namespace Beta.Client.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }
}
