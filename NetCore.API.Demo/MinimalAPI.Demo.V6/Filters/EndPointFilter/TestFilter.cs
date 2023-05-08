
using Common.Custom.Interfaces;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace MinimalAPI.Demo.V6.Filters.EndPointFilter;

public class TestFilter: IFilter
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try 
        {
            using var reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();
            var entity = JsonSerializer.Deserialize<User>(body);
        }
        catch(Exception ex) { }

        context.Response.Headers.Append("TEST_MIDDLEFLAG", "Test");
        await next(context);
    }
}
