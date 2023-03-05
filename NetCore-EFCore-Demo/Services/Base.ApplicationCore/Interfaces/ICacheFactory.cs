using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.ApplicationCore.Interfaces;

public interface ICacheFactory
{
    public IMemoryCache GetOrCreateCache(string name);
    public bool Remove(string name);
    public IMemoryCache Refresh(string name);
}
