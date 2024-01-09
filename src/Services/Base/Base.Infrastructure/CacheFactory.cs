using Base.ApplicationCore.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace Base.Infrastructure;

public class CacheFactory : ICacheFactory
{
    private readonly ConcurrentDictionary<string, IMemoryCache> _caches = new(StringComparer.OrdinalIgnoreCase);

    public IMemoryCache GetOrCreateCache(string name)
    {
        return _caches.GetOrAdd(name, t => CreateCacheCore());
    }

    public IMemoryCache Refresh(string name)
    {
        Remove(name);
        return _caches.GetOrAdd(name, t => CreateCacheCore());
    }

    public bool Remove(string name)
    {
        return _caches.TryRemove(name, out IMemoryCache value);
    }

    protected IMemoryCache CreateCacheCore()
    {
        return new MemoryCache(new MemoryCacheOptions());
    }
}
