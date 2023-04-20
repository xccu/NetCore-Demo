using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Server_Demo;

[DefaultStatusCode(500)]
public class InternalServerErrorRequest : ObjectResult
{
    public InternalServerErrorRequest(object? value) : base(value)
    {
    }
}
