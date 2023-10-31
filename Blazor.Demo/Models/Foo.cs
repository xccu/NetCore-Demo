namespace Models;

public class Foo
{
    public int Id { get { return this.GetHashCode(); }}

    public string Name { get; set; } = "Foo";
}
