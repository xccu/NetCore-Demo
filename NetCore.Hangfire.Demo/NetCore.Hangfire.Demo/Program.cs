using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using RazorPage.Web;
using System.Runtime.CompilerServices;

//see:
//https://www.cnblogs.com/PrintY/p/15807575.html
//https://www.hangfire.io/

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("HangfireConnection");


// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHangfire(config =>
{
    config.UseStorage(new MemoryStorage());
    //config.UseSqlServerStorage(connectionString);
});

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

app.MapRazorPages();

app.UseHangfireServer();
app.UseHangfireDashboard("/hangfire", new DashboardOptions()
{
    //访问面板需要登录，此处也可以不设置
    //Authorization = new[]
    //{
    //    new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
    //        {
    //            SslRedirect = false,          // 是否将所有非SSL请求重定向到SSL URL
    //            RequireSsl = false,           // 需要SSL连接才能访问HangFire Dahsboard。强烈建议在使用基本身份验证时使用SSL
    //            LoginCaseSensitive = false,   //登录检查是否区分大小写
    //            Users = new[]                 //配置登陆账号和密码
    //            {
    //                new BasicAuthAuthorizationUser
    //                {
    //                    Login ="admin",
    //                    PasswordClear="123456"
    //                }
    //            }
    //        })
    //}
});


HangfireJobs.Start();


app.Run();
