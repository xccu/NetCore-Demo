using DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System.Security;

namespace RazorPage.Web.Data;

public class DataSeed
{
    public static async Task SeedAsync(IServiceProvider rootProvider)
    {
        using var scope = rootProvider.CreateScope();

        var userContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        userContext.Add(new User() {  Name = "Weslie", Age = 12, Gender = "male", Race = "Caprinae" });
        userContext.Add(new User() {  Name = "Wolffy", Age = 34, Gender = "male", Race = "Lupo" });
        userContext.Add(new User() {  Name = "Tibby", Age = 11, Gender = "female", Race = "Caprinae" });
        userContext.Add(new User() {  Name = "Sparky", Age = 13, Gender = "male", Race = "Caprinae" });
        userContext.Add(new User() {  Name = "Paddi", Age = 10, Gender = "male", Race = "Caprinae" });
        userContext.Add(new User() {  Name = "Jonie", Age = 13, Gender = "female", Race = "Caprinae" });
        userContext.Add(new User() {  Name = "Slowy", Age = 80, Gender = "male", Race = "Caprinae" });
        userContext.Add(new User() {  Name = "Wolnie", Age = 33, Gender = "female", Race = "Lupo" });
        userContext.Add(new User() {  Name = "Wilie", Age = 5, Gender = "male", Race = "Lupo" });
        await userContext.SaveChangesAsync();

        var movieContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        movieContext.Add(new Movie() { Title = "When Harry Met Sally", ReleaseDate = DateTime.Parse("1989-2-12"), Genre = "Romantic Comedy", Price = 7.99M });
        movieContext.Add(new Movie() { Title = "Ghostbusters ", ReleaseDate = DateTime.Parse("1984-3-13"), Genre = "Comedy", Price = 8.99M});
        movieContext.Add(new Movie() { Title = "Ghostbusters 2", ReleaseDate = DateTime.Parse("1986-2-23"), Genre = "Comedy", Price = 9.99M});
        movieContext.Add(new Movie() { Title = "Rio Bravo", ReleaseDate = DateTime.Parse("1959-4-15"), Genre = "Western", Price = 3.99M });
        await movieContext.SaveChangesAsync();
    }
}
