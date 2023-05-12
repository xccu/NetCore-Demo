using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPage.Demo.Services;

public class CacheService
{
    static MemoryCache _cache;

    public CacheService() 
    {
        if (_cache == null)
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }
    }

    public void SetCache(string key, object value)
    {
        _cache.Set(key, value, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        });
    }

    public object GetCache(string key)
    {
        object val = null;
        if (key != null && _cache.TryGetValue(key, out val))
        {
            return val;
        }
        else
        {
            return default;
        }
    }
}
