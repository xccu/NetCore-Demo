using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom.DI.Demo;

public class Base : IDisposable
{
    public string ID = Guid.NewGuid().ToString();
    public Base()   => Console.WriteLine($"Instance of {GetType().Name} is created.");
    public void Dispose()  => Console.WriteLine($"Instance of {GetType().Name} is disposed.");
   
}