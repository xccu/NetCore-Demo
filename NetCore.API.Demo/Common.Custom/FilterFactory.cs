using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FilterFactory
    {
        public Func<RequestDelegate, RequestDelegate> filter { get; set; }
    }
}
