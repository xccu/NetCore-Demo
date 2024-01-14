﻿
using Device.Infrastructure.Data;
using System.ComponentModel;
using User.Infrastructure.Data;

namespace WebAPI.DBGenerator;

public static class DbContextStore
{
    public static IEnumerable<Func<Type>> Contexts = new Func<Type>[]
    {
        [Description("User Database")]()=> typeof(UserDbContext),
        [Description("Device Database")]()=> typeof(DeviceDbContext)
    };
}
