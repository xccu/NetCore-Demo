using Microsoft.AspNetCore.Authentication;
using MvcClient.Extensions;
using System.IdentityModel.Tokens.Jwt;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(options =>
{
    // using a cookie to locally sign-in the user
    options.DefaultScheme = "Cookies";
    // set the DefaultChallengeScheme to oidc because when we need the user to login
    // we will be using the OpenID Connect protocol.
    options.DefaultChallengeScheme = "oidc";
})
//.AddIdentityServerAuthentication(x => x.RequireHttpsMetadata = false)
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "http://localhost:5001";

    options.ClientId = "mvc";
    options.ClientSecret = "secret";
    options.ResponseType = "code";

    //Getting claims from the UserInfo endpoint
    options.Scope.Add("profile");
    options.GetClaimsFromUserInfoEndpoint = true;
    options.RequireHttpsMetadata = false;
    //add more claims to the test users - and also more identity resources.
    options.ClaimActions.MapUniqueJsonKey("myclaim1", "myclaim1");

    options.SaveTokens = true;
});
builder.Services.AddScoped<IClaimsTransformation, AdditionalClaimsTransformation>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();
app.MapDefaultControllerRoute().RequireAuthorization();

app.Run();