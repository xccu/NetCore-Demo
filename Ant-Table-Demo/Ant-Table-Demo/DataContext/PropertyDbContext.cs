using Ant_Table_Demo.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ant_Table_Demo.DataContext
{
    public class PropertyDbContext : DbContext
    {
        public PropertyDbContext(DbContextOptions<PropertyDbContext> options) : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
    }
}
