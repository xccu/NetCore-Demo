using Base.ApplicationCore.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Base.Infrastructure;

public static class CacheExtensions
{
    public static void SetCache(this IMemoryCache cache, string key, object value, CacheOptions option)
    {
        cache.Set(key, value, new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = option.AbsoluteExpiration,
            AbsoluteExpirationRelativeToNow = option.AbsoluteExpirationRelativeToNow,
            Priority = (Microsoft.Extensions.Caching.Memory.CacheItemPriority)(int)option.Priority,
            SlidingExpiration = option.SlidingExpiration
        });
    }

    public static object GetCache(this IMemoryCache cache, string key)
    {
        object val = null;
        if (key != null && cache.TryGetValue(key, out val))
        {
            return val;
        }
        else
        {
            return default;
        }
    }
}
