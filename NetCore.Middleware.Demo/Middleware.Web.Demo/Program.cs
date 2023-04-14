using Middleware.Web.Demo.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    // Do work that can write to the Response.
    await next.Invoke();
    // Do logging or other work that doesn't write to the Response.
});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Hello\r\n");
//    await next.Invoke();
//    await context.Response.WriteAsync("Hello end\r\n");
//});
app.UseMiddleware<HelloMiddleware>();

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Hello 2\r\n");
    await next.Invoke();
    await context.Response.WriteAsync("Hello 2 end\r\n");
});

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello from 2nd delegate.\r\n");

});



app.Run();