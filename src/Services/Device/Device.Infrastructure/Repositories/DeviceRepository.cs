using Base.Infrastructure.Repositories;
using Device.ApplicationCore.Interfaces.Repositories;
using Device.Infrastructure.Data;

namespace Device.Infrastructure.Repositories;

public class DeviceRepository : BaseRepository<ApplicationCore.Entities.Device>, IDeviceRepository
{
    public DeviceRepository(DeviceContext dbContext) : base(dbContext)
    {

    }
}

