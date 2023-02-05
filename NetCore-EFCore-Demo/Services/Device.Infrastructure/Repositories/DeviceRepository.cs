using Base.Infrastructure.Repositories;
using Device.ApplicationCore.Interfaces.Repositories;
using Device.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.Infrastructure.Repositories;

public class DeviceRepository : BaseRepository<ApplicationCore.Entities.Device>, IDeviceRepository
{
    public DeviceRepository(DeviceContext dbContext) : base(dbContext)
    {

    }
}

