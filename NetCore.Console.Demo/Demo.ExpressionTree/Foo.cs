using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ExpressionTree;

public class Foo
{
    private int Id { get; set;}
    private string Name { get; set; }

    public Foo(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void print()
    {
        Console.WriteLine(output());
    }

    public string output()
    {
        return $"id-{Id}; name-{Name}";
    }
}
