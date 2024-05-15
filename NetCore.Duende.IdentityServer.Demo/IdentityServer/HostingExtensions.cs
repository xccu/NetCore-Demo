using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // uncomment if you want to add a UI
        builder.Services.AddRazorPages();

        #region uncomment if you want to store config in memory
        builder.Services.AddIdentityServer(options => 
        {
            options.Authentication.CookieSameSiteMode = SameSiteMode.Lax;
        }).AddInMemoryIdentityResources(Config.IdentityResources)
          .AddInMemoryApiScopes(Config.ApiScopes)
          .AddInMemoryClients(Config.Clients)
          .AddTestUsers(TestUsers.Users);
        #endregion

        #region uncomment if you want to store config in SqlServer db
        //var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
        //const string connectionString = @"Data Source=.;Database=IdentityServerDb;Integrated Security=True;trustServerCertificate=true;";

        //builder.Services.AddIdentityServer(options => 
        //{
        //    options.Authentication.CookieSameSiteMode = SameSiteMode.Lax;
        //}).AddConfigurationStore(options =>
        //  {
        //      options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
        //          sql => sql.MigrationsAssembly(migrationsAssembly));
        //  })
        //  .AddOperationalStore(options =>
        //  {
        //      options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
        //          sql => sql.MigrationsAssembly(migrationsAssembly));
        //  })
        //  .AddTestUsers(TestUsers.Users);
        #endregion

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // uncomment if you want to store config in SqlServer db
        //InitializeDatabase(app);

        // uncomment if you want to add a UI
        app.UseStaticFiles();
        app.UseRouting();
            
        app.UseIdentityServer();

        // uncomment if you want to add a UI
        app.UseAuthorization();
        app.MapRazorPages().RequireAuthorization();

        return app;
    }

    //https://docs.duendesoftware.com/identityserver/v7/quickstarts/4_ef/
    private static void InitializeDatabase(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope())
        {
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes)
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }

}
