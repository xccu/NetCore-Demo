using Common.Custom.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinimalAPI.Demo.V6.Bindings;
using MinimalAPI.Demo.V6.Filters.EndPointFilter;
using MinimalAPI.Server.API;
using System;
using System.Security.Claims;
using System.Xml.Linq;

namespace MinimalAPI.Demo.V6.Extensions;

public static class ParameterBindingAPIExtension
{
    public static WebApplication UseParameterBindingAPI(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        //app.MapPut("/people/{id:int}", (int id, bool notify, Person person, PeopleService peopleService) => { });
        //url:search?q=text
        app.MapGet("/search", (string q) => { return q; });
        app.MapGet("/search2", ([FromQuery(Name = "q")] string searchText) => { return searchText; });
        app.MapGet("/people", (int pageIndex, int itemsPerPage) =>{});
        //This won't compile
        //app.MapGet("/people1", (int pageIndex = 0, int itemsPerPage = 50) => { });
        app.MapGet("/people2", SearchMethod);

        #region Special bindings
        app.MapGet("/products", (HttpContext context, HttpRequest req,HttpResponse res, ClaimsPrincipal user) 
            => {return "Special bindings";});
        #endregion

        #region Custom binding

        //GET https://localhost:7069/navigate?location=43.8427,7.8527
        app.MapGet("/navigate", (Location location) => $"Location: { location.Latitude}, { location.Longitude}");

        //GET https://localhost:7069/navigate2?lat=43.8427&lon=7.8527
        app.MapGet("/navigate2", (LocationBind location) => $"Location: {location.Latitude}, {location.Longitude}");
        #endregion

        return app;
    }

    private static string SearchMethod(int pageIndex = 0, int itemsPerPage = 50)
        => $"Sample result for page {pageIndex} getting {itemsPerPage} elements ";
}

public class PeopleService { }
public record class Person(string FirstName, string LastName) { }