﻿using Base.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.ApplicationCore.Interfaces.Repositories;

public interface IDeviceRepository : IRepository<Entities.Device>
{
}
