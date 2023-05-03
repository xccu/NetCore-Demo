using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Common;

[DefaultStatusCode(500)]
public class InternalServerErrorRequest : ObjectResult
{
    public InternalServerErrorRequest(object? value) : base(value)
    {
    }
}
