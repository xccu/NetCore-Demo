using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Authorization;

public class BlazorMinimumAgeRequirement : IAuthorizationRequirement
{
    public int MinimumAge { get; }

    public BlazorMinimumAgeRequirement(int minimumAge)
    {
        MinimumAge = minimumAge;
    }
}
