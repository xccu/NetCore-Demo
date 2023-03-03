using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Infrastructure;

public class CacheOptions
{

    public DateTimeOffset? AbsoluteExpiration { get; set; }


    public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }


    public CacheItemPriority Priority { get; set; }


    public TimeSpan? SlidingExpiration { get; set; }


    public string KeyPrefix { get; set; }


    public CacheOptions()
    {
        this.Priority = CacheItemPriority.Normal;
        this.KeyPrefix = "Test";
    }

}