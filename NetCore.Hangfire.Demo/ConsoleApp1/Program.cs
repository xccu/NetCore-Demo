
var foo = new Foo();
foo.Bar.IncraseId();
Console.WriteLine(foo.Bar.Id);
//List<int> list = new List<int>();

Console.ReadKey();

class Foo
{
    public readonly Bar Bar = new();
}


struct Bar
{
    public int Id { get; set; }

    public void IncraseId()
    {
        Id++;
    }
}