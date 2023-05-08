using Common.API;
using DataAccess;
using MinimalAPI.Demo.V7.Filters.EndPointFilter;

namespace Microsoft.Extensions.DependencyInjection;

public static class UserAPIExtension
{ 
    public static WebApplication UseMinimalUserAPI(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var API = scope.ServiceProvider.GetRequiredService<UserAPI>();

        app.MapGet("/api/User/GetAll", API.GetAll).AddEndpointFilter<TestFilter>();
        app.MapGet("/api/User/{Id}", API.GetById);
        app.MapPost("/api/User/Add", API.Add).AddEndpointFilter<ValidationFilter<User>>();
        app.MapPut("/api/User/Update", API.Update);
        app.MapDelete("/api/User/Delete", API.Delete);
        app.MapGet("/api/User/GetException", API.GetException).AddEndpointFilter<ExceptionRecoverFilter>();
        app.MapGet("/api/User/Ok", API.Ok);
        app.MapPost("/api/User/Binder", API.Binder);

        return app;
    }
}
