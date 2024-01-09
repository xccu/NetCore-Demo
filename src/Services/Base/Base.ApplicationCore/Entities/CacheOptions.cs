using Microsoft.Extensions.Caching.Memory;

namespace Base.ApplicationCore.Entities;

public class CacheOptions
{

    public DateTimeOffset? AbsoluteExpiration { get; set; }


    public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }


    public CacheItemPriority Priority { get; set; }


    public TimeSpan? SlidingExpiration { get; set; }


    public string KeyPrefix { get; set; }


    public CacheOptions()
    {
        Priority = CacheItemPriority.Normal;
        KeyPrefix = "Test";
    }

}