using Base.Infrastructure;
using Microsoft.Extensions.Caching.Memory;

namespace WebAPI.Extensions;

public static class CacheExtensions
{
    public static void SetCache(this IMemoryCache cache, string key, object value, CacheOptions option)
    {
        cache.Set(key, value, new MemoryCacheEntryOptions
        {
            SlidingExpiration = option.AbsoluteExpirationRelativeToNow
        });
    }

    public static object GetCache(this IMemoryCache cache, object key)
    {
        object val = null;
        if (key != null && cache.TryGetValue(key, out val))
        {
            return val;
        }
        else
        {
            return default(object);
        }
    }
}