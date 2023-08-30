using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Decorator;

public class DecoratorDemo
{
    public static void Run()
    {
        var svcs = new ServiceCollection();
        svcs.AddTransient<Foo>();
        svcs.AddTransient<IWrite, Write1>();
        svcs.Decorate<IWrite, Write2>();
        svcs.Decorate<IWrite, Write3>();

        var provider = svcs.BuildServiceProvider();

        var iwrite = provider.GetRequiredService<IWrite>();

        iwrite.Write();

        Console.ReadKey();
    }
}
