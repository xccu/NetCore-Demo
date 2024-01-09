using Microsoft.EntityFrameworkCore;

namespace Device.Infrastructure.Data;

public class DeviceContext : DbContext
{
    public DeviceContext(DbContextOptions<DeviceContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<ApplicationCore.Entities.Device>();
    }

    public virtual DbSet<ApplicationCore.Entities.Device> Device { get; set; }

}
