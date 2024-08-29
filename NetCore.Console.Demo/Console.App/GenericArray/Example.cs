
namespace ConsoleApplication.GenericArray;

public class Example<T>
{
    public static void ObjectArrayMethod(params object?[]? args) 
    {
        Console.WriteLine($"ObjectArrayMethod args length：{args.Length}");
        Console.WriteLine($"ObjectArrayMethod args：{args}");
    }

    public static void GengerictArrayMethod(params T?[]? args)
    {
        Console.WriteLine($"GengerictArrayMethod args length：{args.Length}");
        Console.WriteLine($"GengerictArrayMethod args：{args}");
    }

    public static void Run(params T?[]? args) 
    {
        ObjectArrayMethod(args);
        GengerictArrayMethod(args);
    }
}

