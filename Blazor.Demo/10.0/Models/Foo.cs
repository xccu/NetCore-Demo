using System.Text.Json.Serialization;

namespace Models;

public class Foo
{
    public Foo() { }

    public int Id { get { return this.GetHashCode(); }}

    public string Name { get; set; } = "Foo";

    public int Number { get; set; }
}
