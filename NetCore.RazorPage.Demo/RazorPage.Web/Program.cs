//see:
//https://learn.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-6.0&tabs=visual-studio
//https://learn.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/?view=aspnetcore-6.0
//https://www.learnrazorpages.com/razor-pages/handler-methods

using DataAccess;
using Microsoft.EntityFrameworkCore;
using RazorPage.Web.Data;
using Common.Filter;
using Common.Convention;
using Common.Authorization;
using Microsoft.AspNetCore.Authorization;
using Common.Authentication;
using RazorPage.Web;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Reflection;
using Common.Attributes;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("Default");

// Add services to the container.

#region Authentication and Authorization
var permissionRequirement = new MinimumAgeRequirement(18);

//授权
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AtLeast18", policy => policy.Requirements.Add(permissionRequirement));
});
// 注入权限处理器
builder.Services.AddTransient<IAuthorizationHandler, MinimumAgeHandler>();

// Default认证方案(不手动Login)
builder.Services.AddAuthentication("default")
                .AddScheme<DefaultSchemeOptions, DefaultHandler>("default", null, null);
#endregion

// dynamic add [Authorize] in Razorpage
builder.Services.AddSingleton<IActionDescriptorProvider, AuthorizeDescriptorProvider>();
// use for PermissionButton
builder.Services.AddHttpContextAccessor(); 
builder.Services.AddRazorPages(options =>
{
    #region Filters
    options.Conventions.AddPageApplicationModelConvention( //add filter for special page
    "/Test/Authorize",
    model => model.Filters.Add(new HttpAsyncPageFilter()));
    options.Conventions.AddFolderApplicationModelConvention( //add filter for special folder path
        "/Movies",
        model => model.Filters.Add(new HttpAsyncPageFilter()));
    options.Conventions.ConfigureFilter(new AddHeaderWithFactory());
    #endregion

    #region Authorization
    options.Conventions.AuthorizePage("/Test/Authorize", "AtLeast18");
    options.Conventions.AuthorizeFolder("/Movies", "AtLeast18");
    #endregion

    #region Route and ModelConvention
    options.Conventions.Add(new GlobalTemplatePageRouteModelConvention());
    options.Conventions.AddPageRouteModelConvention("/About", ModelConventions.About);
    options.Conventions.AddFolderRouteModelConvention("/OtherPages", ModelConventions.OtherPages);
    options.Conventions.AddPageRoute("/Test/Contact", "TheContactPage/{text?}");
    options.Conventions.AddPageRoute("/Index", "home/Index");
    #endregion
})
.AddMvcOptions(options => // add global filter 
{
    options.Filters.Add(new ValidationAsyncPageFilter());
})
.AddViewOptions(options => //disable client validation
{
    options.HtmlHelperOptions.ClientValidationEnabled = false;
});



#region test RootDirectory
//builder.Services.AddRazorPages(options =>
//{
//    options.RootDirectory = "/MyPages";
//    options.Conventions.AddPageRoute("/Admin", "");
//});
#endregion

#region UseInMemoryDatabase
//builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("MemoryDemoDb"));
//builder.Services.AddDbContext<MovieDbContext>(options => options.UseInMemoryDatabase("MemoryDemoDb"));
//builder.Services.AddDbContext<DepartmentDbContext>(options => options.UseInMemoryDatabase("MemoryDemoDb"));
#endregion

#region UseSqlServer
//Concurrency Test must use SqlServer 
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<DepartmentDbContext>(options => options.UseSqlServer(connectionString));
#endregion

var app = builder.Build();

await DataSeed.SqlServerSeedAsync(app.Services);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios,
    // see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//get handlerName
app.Use(async (context, next) =>
{
    var endpoint = context.GetEndpoint();

    if (endpoint is not null)
    {
        var page = endpoint.Metadata.GetOrderedMetadata<ActionDescriptor>()[0];

        if (page is CompiledPageActionDescriptor) //Check whether is a razorPage
        {
            //Create RouteData
            var routeData = new RouteData(context.Request.RouteValues);
            //Create ActionContext
            ActionContext actionContext = new ActionContext(context, routeData, page);
            //Convert to CompiledPageActionDescriptor
            var actionDescriptor = Unsafe.As<CompiledPageActionDescriptor>(page);
            //Create PageContext
            PageContext pageContext = new PageContext(actionContext)
            {
                ActionDescriptor = actionDescriptor
            };
            //Get IPageHandlerMethodSelector
            var selector = context.RequestServices.GetRequiredService<IPageHandlerMethodSelector>();
            //Get HandlerMethodDescriptor
            HandlerMethodDescriptor result = selector.Select(pageContext);
            //get handlerName like "OnPostParam"
            string handlerName = $"On{result.HttpMethod}{result.Name}";
            handlerName = result.MethodInfo.Name;
            var method = actionDescriptor.HandlerMethods.FirstOrDefault(t => t.MethodInfo.Name == handlerName);

            string policy = method?.MethodInfo.GetCustomAttribute<PermissionAttribute>()?.Policy;
        }     
    }
    await next();
});


//UseAuthentication must before UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
//app.MapControllerRoute("mvc", "{controller=users}/{action=Index}");
app.ViewEndpoints();
app.Run();
