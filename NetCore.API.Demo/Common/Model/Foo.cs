using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model;

public class Foo
{
    public int Id { get; set; } = 1;
    public string Text { get; set; } = "Foo text";
    public Order Order { get; set; }

    public Foo() { }
    public Foo(int id,string text)
    {
        Id = id;
        Text = text;
    }
}

public enum Order
{
    Ascending,
    Descending,
}
