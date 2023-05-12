using Common.Custom.Attributes;
using Common.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using MinimalAPI.Demo.V6.Bindings;
using MinimalAPI.Demo.V6.Filters.EndPointFilter;
using System;
using System.Security.Claims;
using System.Xml.Linq;

namespace MinimalAPI.Demo.V6.Extensions;

public static class ParameterBindingAPIExtension
{
    public static WebApplication UseParameterBindingAPI(this WebApplication app)
    {
        //var scope = app.Services.CreateScope();

        #region ParameterBinding
        //url:people/1?notify=true
        app.MapPut("/people/{id:int}", (int id, bool notify) => { return "ID:"+id; })
            .WithTags("Parameter Binding");
        //url:search?q=text
        app.MapGet("/search", (string q) => { return q; })
            .WithTags("Parameter Binding");
        //url:search2?q=text
        app.MapGet("/search2", ([FromQuery(Name = "q")] string searchText) => { return searchText; })
            .WithTags("Parameter Binding"); ;
        //url:people?pageIndex=1&itemsPerPage=10
        app.MapGet("/people", (int pageIndex, int itemsPerPage) =>{})
            .WithTags("Parameter Binding");
        //url:people2?pageIndex=0&itemsPerPage=50
        app.MapGet("/people2", SearchMethod)
            .WithTags("Parameter Binding");
        //This won't compile
        //app.MapGet("/people1", (int pageIndex = 0, int itemsPerPage = 50) => { });

        #endregion

        #region Special bindings
        //url:products
        app.MapGet("/products", (HttpContext context, HttpRequest req,HttpResponse res, ClaimsPrincipal user) 
            => {return "Special bindings";}).WithTags("Special Binding");
        #endregion

        #region Custom binding
        //GET https://localhost:7069/navigate?location=43.8427,7.8527
        app.MapGet("/navigate", (Location location) => $"Location: { location.Latitude}, { location.Longitude}").WithTags("Custom binding");

        //GET https://localhost:7069/navigate2?lat=43.8427&lon=7.8527
        app.MapGet("/navigate2", (LocationBind location) => $"Location: {location.Latitude}, {location.Longitude}").WithTags("Custom binding");

        app.MapPost("post/navigate2", (LocationBind location) => $"Location: {location.Latitude}, {location.Longitude}").WithTags("Custom binding");
        #endregion

        return app;
    }

    private static string SearchMethod(int pageIndex = 0, int itemsPerPage = 50)
        => $"Sample result for page {pageIndex} getting {itemsPerPage} elements ";
}

public class PeopleService { }
public record class Person(string FirstName, string LastName) { }