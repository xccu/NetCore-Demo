using Microsoft.AspNetCore.Authorization;
using OpenIddict.Validation.AspNetCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region use default scheme
////Add the authentication and authorization services
//builder.Services.AddAuthentication()
//    .AddJwtBearer(options =>
//    {
//        options.Authority = "https://localhost:7139";
//        options.TokenValidationParameters.ValidateAudience = false;
//        //options.RequireHttpsMetadata = false; //config for http 
//    });
////Add an Authorization Policy
//builder.Services.AddAuthorization(options =>
//{

//    options.AddPolicy("ApiScope", policy =>
//    {
//        policy.RequireAuthenticatedUser();
//        policy.RequireClaim("scope", "api1");
//    });
//});
#endregion

#region use Openiddict
builder.Services.AddOpenIddict()
    .AddValidation(options =>
    {
        // Note: the validation handler uses OpenID Connect discovery
        // to retrieve the address of the introspection endpoint.
        options.SetIssuer("https://localhost:7139");
        //options.AddAudiences("api1");

        // Configure the validation handler to use introspection and register the client
        // credentials used when communicating with the remote introspection endpoint.
        options.SetClientId("api1")
               .SetClientSecret("api1-secret");

        // Register the System.Net.Http integration.
        options.UseSystemNetHttp();

        // Register the ASP.NET Core host.
        options.UseAspNetCore();
    });

builder.Services.AddCors();
builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
builder.Services.AddAuthorization();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.MapGet("identity", (ClaimsPrincipal user) =>
//{
//    return user.Claims.Select(c => new { c.Type, c.Value });
//}).RequireAuthorization("ApiScope");

app.MapGet("identity", [Authorize](ClaimsPrincipal user) =>
{
    return user.Claims.Select(c => new { c.Type, c.Value });
});


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
