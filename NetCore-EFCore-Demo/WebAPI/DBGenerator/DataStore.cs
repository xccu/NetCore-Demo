
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
        var context = provider.GetRequiredService<UserDbContext>();
        context.Add(new Users.User() { Name = "Weslie", Password= "Psd%123", Age= 12, Gender= "Male", Race= "Caprinae" });
        context.Add(new Users.User() { Name = "Wolffy", Password = "Psd%123", Age = 34, Gender = "Male", Race = "Lupo" });
        context.Add(new Users.User() { Name = "Paddi", Password = "Psd%123", Age = 10, Gender = "Male", Race = "Caprinae" });
        context.Add(new Users.User() { Name = "Tibby", Password = "Psd%123", Age = 11, Gender = "Female", Race = "Caprinae" });
        context.Add(new Users.User() { Name = "Sparky", Password = "Psd%123", Age = 13, Gender = "Male", Race = "Caprinae" });
        context.Add(new Users.User() { Name = "Jonie", Password = "Psd%123", Age = 13, Gender = "Female", Race = "Caprinae" });
        context.Add(new Users.User() { Name = "Slowy", Password = "Psd%123", Age = 80, Gender = "Male", Race = "Caprinae" });
        context.Add(new Users.User() { Name = "Wolnie", Password = "Psd%123", Age = 33, Gender = "Female", Race = "Lupo" });
        context.Add(new Users.User() { Name = "Wilie", Password = "Psd%123", Age = 3, Gender = "Male", Race = "Lupo" });

        context.SaveChanges();
    }

    private static void WriteDevice(IServiceProvider provider)
    {
        var context = provider.GetRequiredService<DeviceDbContext>();
        context.Add(new Devices.Device() { name = "DELL-Laptop", description = "Laptop", deviceNumber = "H000001", registedDate = Convert.ToDateTime("2023-1-1") });
        context.Add(new Devices.Device() { name = "HP-Screen", description = "Screen", deviceNumber = "H000002", registedDate = Convert.ToDateTime("2023-1-1") });
        context.Add(new Devices.Device() { name = "Desk", description = "Desk", deviceNumber = "H000003", registedDate = Convert.ToDateTime("2023-1-1") });
        context.Add(new Devices.Device() { name = "Chair", description = "Chair", deviceNumber = "H000004", registedDate = Convert.ToDateTime("2023-1-1") });
        context.Add(new Devices.Device() { name = "MircoPhone", description = "MircoPhone", deviceNumber = "H000005", registedDate = Convert.ToDateTime("2023-1-1") });

        context.SaveChanges();
    }
}