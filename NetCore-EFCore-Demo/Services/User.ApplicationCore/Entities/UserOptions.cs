using Base.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.ApplicationCore.Entities
{
    public class UserOptions
    {
        public bool EnableCache { get; set; } = false;
        public CacheOptions CacheOptions { get; set; } = new CacheOptions();
    }
}
