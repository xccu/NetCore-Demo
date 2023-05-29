using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.Runtime.CompilerServices;

//see:
//https://www.cnblogs.com/PrintY/p/15807575.html
//https://www.hangfire.io/

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("HangfireConnection");


// Add services to the container.
builder.Services.AddRazorPages();

// Add Hangfire services.
builder.Services.AddHangfire(cig => cig.UseSqlServerStorage(connectionString));

//use as hangfire Server
// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    var endpoint = context.GetEndpoint();

    if(endpoint is not null)
    {

        var page = endpoint.Metadata.GetOrderedMetadata<ActionDescriptor>()[0];

        var t = endpoint.Metadata.GetOrderedMetadata<DataTokensMetadata>();

        var routeData = new RouteData(context.Request.RouteValues );

        ActionContext actionContext = new ActionContext(context, routeData, page);

        var selector = context.RequestServices.GetRequiredService<IPageHandlerMethodSelector>();

        //var item = context.RequestServices.GetRequiredService<PageActionInvokerCache>();

        var qwe = Unsafe.As<CompiledPageActionDescriptor>(page);

        PageContext pageContext = new PageContext(actionContext)
        {
            ActionDescriptor = qwe,
            //ValueProviderFactories = new CopyOnWriteList<IValueProviderFactory>(_valueProviderFactories),
            //ViewData = item.ViewDataFactory(_modelMetadataProvider, actionContext.ModelState),
            //ViewStartFactories = item.ViewStartFactories.ToList()
        };

        var result = selector.Select(pageContext);

    }


    await next();

});

app.UseAuthorization();

app.UseHangfireDashboard("/hangfire", new DashboardOptions());

app.MapRazorPages();

app.UseHangfireDashboard();

//JobService.Start();

app.Run();