using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ConsoleApplication.GuidGenerator;

public class GuidGenerator
{
    public void Run()
    {
        SequentialGuidValueGenerator generator = new SequentialGuidValueGenerator();

        for (int i = 0; i < 23; i++)
        {
            Console.WriteLine($"CodeId =\"{generator.Next(null)}\", ");
        }
    }
}
