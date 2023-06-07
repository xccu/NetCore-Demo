
using Device.Infrastructure.Data;
using User.Infrastructure.Data;
using Users = User.ApplicationCore.Entities;
using Devices = Device.ApplicationCore.Entities;

namespace WebAPI.DBGenerator;

public static class DataStore
{
    public static void ImportData(IServiceProvider provider)
    {
        using var scope = provider.CreateScope();
        provider = scope.ServiceProvider;
        WriteUser(provider);
        WriteDevice(provider);
    }

    private static void WriteUser(IServiceProvider provider)
    {
        var context = provider.GetRequiredService<UserContext>();
        context.Add(new Users.User() { name = "Weslie", password= "Psd%123", age= 12, gender= "Male", race= "Caprinae" });
        context.Add(new Users.User() { name = "Wolffy", password = "Psd%123", age = 34, gender = "Male", race = "Lupo" });
        context.Add(new Users.User() { name = "Paddi", password = "Psd%123", age = 10, gender = "Male", race = "Caprinae" });
        context.Add(new Users.User() { name = "Tibby", password = "Psd%123", age = 11, gender = "Female", race = "Caprinae" });
        context.Add(new Users.User() { name = "Sparky", password = "Psd%123", age = 13, gender = "Male", race = "Caprinae" });
        context.Add(new Users.User() { name = "Jonie", password = "Psd%123", age = 13, gender = "Female", race = "Caprinae" });
        context.Add(new Users.User() { name = "Slowy", password = "Psd%123", age = 80, gender = "Male", race = "Caprinae" });
        context.Add(new Users.User() { name = "Wolnie", password = "Psd%123", age = 33, gender = "Female", race = "Lupo" });
        context.Add(new Users.User() { name = "Wilie", password = "Psd%123", age = 3, gender = "Male", race = "Lupo" });

        context.SaveChanges();
    }

    private static void WriteDevice(IServiceProvider provider)
    {
        var context = provider.GetRequiredService<DeviceContext>();
        context.Add(new Devices.Device() { name = "DELL-Laptop", description = "Laptop", deviceNumber = "H000001", registedDate = Convert.ToDateTime("2023-1-1") });
        context.Add(new Devices.Device() { name = "HP-Screen", description = "Screen", deviceNumber = "H000002", registedDate = Convert.ToDateTime("2023-1-1") });
        context.Add(new Devices.Device() { name = "Desk", description = "Desk", deviceNumber = "H000003", registedDate = Convert.ToDateTime("2023-1-1") });
        context.Add(new Devices.Device() { name = "Chair", description = "Chair", deviceNumber = "H000004", registedDate = Convert.ToDateTime("2023-1-1") });
        context.Add(new Devices.Device() { name = "MircoPhone", description = "MircoPhone", deviceNumber = "H000005", registedDate = Convert.ToDateTime("2023-1-1") });

        context.SaveChanges();
    }
}