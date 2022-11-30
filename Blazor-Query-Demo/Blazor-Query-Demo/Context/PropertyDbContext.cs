using Blazor_Query_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Query_Demo.Context
{
    public class PropertyDbContext : DbContext
    {
        public PropertyDbContext(DbContextOptions<PropertyDbContext> options) : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
    }


}
