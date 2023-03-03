using Base.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Infrastructure
{
    public class UserOption
    {
        public bool EnableCache { get; set; } = false;
        public CacheOptions CacheOptions { get; set; } = new CacheOptions();
    }
}
