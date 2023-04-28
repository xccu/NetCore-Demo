using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Options;

public class FilterOptions
{
    /// <summary>
    /// The path to match.
    /// </summary>
    public PathString PathMatch { get; set; }

    /// <summary>
    /// The branch taken for a positive match.
    /// </summary>
    public RequestDelegate? Branch { get; set; }

    public Func<RequestDelegate, RequestDelegate> filter { get; set; }

    /// <summary>
    /// If false, matched path would be removed from Request.Path and added to Request.PathBase
    /// Defaults to false.
    /// </summary>
    //public bool PreserveMatchedPathSegment { get; set; }


}
