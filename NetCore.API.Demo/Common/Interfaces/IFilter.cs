using Microsoft.AspNetCore.Http;

namespace Common.Interfaces;

public interface IFilter
{
    Task InvokeAsync(HttpContext context, RequestDelegate next);
}
