using DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System.Security;

namespace DataAccess;

public class DataSeed
{
    public static async Task SeedAsync(IServiceProvider rootProvider)
    {
        using var scope = rootProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        context.Add(new User() {  name = "Weslie", age = 12, gender = "male", race = "Caprinae" });
        context.Add(new User() {  name = "Wolffy", age = 34, gender = "male", race = "Lupo" });
        context.Add(new User() {  name = "Tibby", age = 11, gender = "female", race = "Caprinae" });
        context.Add(new User() {  name = "Sparky", age = 13, gender = "male", race = "Caprinae" });
        context.Add(new User() {  name = "Paddi", age = 10, gender = "male", race = "Caprinae" });
        context.Add(new User() {  name = "Jonie", age = 13, gender = "female", race = "Caprinae" });
        context.Add(new User() {  name = "Slowy", age = 80, gender = "male", race = "Caprinae" });
        context.Add(new User() {  name = "Wolnie", age = 33, gender = "female", race = "Lupo" });
        context.Add(new User() {  name = "Wilie", age = 5, gender = "male", race = "Lupo" });
        await context.SaveChangesAsync();

    }
}
