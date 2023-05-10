using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace Microsoft.AspNetCore.Builder
{
    public static  class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
           
            ExceptionHandlerOptions exceptionHandlerOptions = new ExceptionHandlerOptions { ExceptionHandler = HandleException };
            return app.UseExceptionHandler(exceptionHandlerOptions);
               
        }

        static async Task HandleException(HttpContext context)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error != null)
            {
                var ex = exceptionHandlerPathFeature?.Error;
                await WriteExceptionToHttpResponse(context, ex);
            }
        }

        internal static async Task WriteExceptionToHttpResponse(HttpContext httpContext, Exception ex)
        {          

            var problemDetails = new ProblemDetails()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7807",
                Instance = "about:blank",
                Status = StatusCodes.Status500InternalServerError,
                Title = "Test Exception",
                Detail = $"InternalServerError:{ex.Message}",
            };
            var errorJson = JsonSerializer.Serialize(problemDetails);

            httpContext.Response.Headers.Append("TEST_FLAGS", "ERROR");
            
        }
    }
}
