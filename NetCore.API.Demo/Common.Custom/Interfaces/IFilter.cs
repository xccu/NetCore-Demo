using Microsoft.AspNetCore.Http;

namespace Common.Custom.Interfaces;

public interface IFilter
{
    Task InvokeAsync(HttpContext context, RequestDelegate next);
}
